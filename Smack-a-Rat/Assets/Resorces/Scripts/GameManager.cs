using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using System;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;

public class GameManager : MonoBehaviour
{
    public static InfoApi _infoApi;
    public SubmitScoreApi SubmitScoreApi;
    public SubmitHitApi SubmitHitApi;
    public List<GameObject> Rats;
    public List<GameObject> Locations;
    public int lives = 3;
    public List<GameObject> ShowLives;
    public int Score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    public float ratActiveSpeed = 35f;
    private float ratSpawnSpeed = 25f;
    private float increaseSpawnSpeed = 10.0f;
    public float increaseActiveSpeed = 10.0f;
    private float timer = 0f;
    private float totalTime = 0f;
    private bool dead = false;

    private List<float> TimesLost = new List<float>();
    [SerializeField] private GameObject LoseScreen;
    [SerializeField] private GameObject TimeLine;
    [SerializeField] private GameObject TimePoint;

    public int amountTotal;
    public int amountHit;
    void Start()
    {
        _infoApi = FindObjectOfType<InfoApi>();
        SubmitScoreApi = FindObjectOfType<SubmitScoreApi>();
        SubmitHitApi = FindObjectOfType<SubmitHitApi>();
    }
    public void LostLifeTime(int ratId)
    {
        _infoApi.ratKind.Add(ratId);
        _infoApi.time_Taken.Add((int)math.round(totalTime));
        TimesLost.Add(totalTime);
        StartCoroutine(SubmitHitApi.SubmitHit(_infoApi.session_id, _infoApi.ratKind[TimesLost.Count-1], _infoApi.time_Taken[TimesLost.Count - 1]));
        string ratKind = "";
        switch (ratId)
        {
            case 0:
                ratKind = "Rat";
                break;
            case 1:
                ratKind = "Special Rat";
                break;
            case 2:
                ratKind = "Bomb Rat";
                break;
            default:
                ratKind = "";
                break;
        }
        Debug.Log("Rat: " + ratKind + ". And When: " + _infoApi.time_Taken[TimesLost.Count - 1]);
    }
    void Update()
    {
        Ui();
        if (timer > ratSpawnSpeed)
        {
            timer = 0f;
            SpawnWitchRat();
            Diffeculty();
        }

        if (lives > 0)
        {
            timer += Time.deltaTime * increaseSpawnSpeed;
            totalTime += Time.deltaTime;
        }else if (!dead && lives<=0)
        {
            dead = true;
            LoseScreen.SetActive(true);
            Defeat();
        }
        if (!dead && Input.GetMouseButtonDown(0))
        {
            Acuracy();
        }
    }
    private void Defeat()
    {
        _infoApi.time_taken = (int)Math.Round(totalTime);
        _infoApi.accuracy = (float)Math.Round(MathProcent(amountHit, amountTotal), 1);
        _infoApi.score = Score;
        for (int i = 0; i < TimesLost.Count; i++)
        {

            float temp =  MathProcent(TimesLost[i], totalTime);
            //amount hit and when you get hit TimesLost[i] with what  and what procentage
            temp = temp*10 - 500;
            Instantiate(TimePoint, TimeLine.transform.position + Vector3.right * temp, quaternion.identity, TimeLine.transform);

        }
        Debug.Log("Score: "+_infoApi.score+". Total time: "+ _infoApi.time_taken+". Accuracy: "+ _infoApi.accuracy);
        StartCoroutine(SubmitScoreApi.SubmitScore(_infoApi.session_id, _infoApi.score, _infoApi.time_taken, _infoApi.accuracy));
    }
    private void Acuracy()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
        if (hit2D)
        {
            if (hit2D.collider.CompareTag("Rat"))
            {
                amountHit++;
            }
            amountTotal++;
        }
    }
    private float MathProcent(float point,float end)
    {
        if (end <= 0.0001f || point <= 0.00001f) return 0.0f;
        return point / end * 100f;
    }
    private void Diffeculty()
    {
        increaseSpawnSpeed *= 1.02f;
        increaseActiveSpeed *= 1.01f;
    }
    private void Ui()
    {
        for (int i = 0; i < ShowLives.Count; i++)
        {
            if (lives - i -1 >= 0)
            {
                ShowLives[i].SetActive(true);
            }
            else
            {
                ShowLives[i].SetActive(false);
            }
        }
        scoreText.text = Score + " :Score";
        timeText.text = "Time: " + Math.Round(totalTime);
    }
    private void SpawnWitchRat()
    {
        int rand = UnityEngine.Random.Range(0, 11);
        
        int chosenRat = 0;//Spawns normal rat
        if (rand == 9)//Spawns Explosive rat
        {
            chosenRat = 1;
        }
        else if (rand == 10)//Spawns Special rat
        {
            chosenRat = 2;
        }
        if (CheckAvalableLocations())
        {
            Instantiate(Rats[chosenRat], Locations[ChoosLocation()].transform);
        }
    }
    private bool CheckAvalableLocations()
    {
        if (ChoosLocation()>=0)
        {
            return true;
        }else
        {
            return false;
        }
    }
    private int ChoosLocation()
    {
        List<int> LeftLocations = new List<int>();
        for (int i = 0; i < Locations.Count; i++)
        {
            GameObject temp = Locations[i];
            if (temp.transform.childCount == 0)
            {
                LeftLocations.Add(i);
            }
        }
        if(LeftLocations.Count != 0)
        {
            int rand = UnityEngine.Random.Range(0, LeftLocations.Count);
            return LeftLocations[rand]; 
        }else
        {
            return -1;
        }
    }

}
