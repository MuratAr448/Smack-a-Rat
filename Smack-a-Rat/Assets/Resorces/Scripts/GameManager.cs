using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using System;

public class GameManager : MonoBehaviour
{
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
    void Start()
    {
        
    }
    public void LostLifeTime()
    {
        TimesLost.Add(totalTime);
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
    }
    private void Defeat()
    {
        for (int i = 0; i < TimesLost.Count; i++)
        {
            float temp =  MathProcent(TimesLost[i], totalTime);
            Debug.Log("Time got hit: " + Math.Round(TimesLost[i]) + ", Procent: " + Math.Round(temp * 100, 1));
            temp = 1000 - (1000 * temp) - 500;
            Instantiate(TimePoint, TimeLine.transform.position + Vector3.right * -temp, quaternion.identity, TimeLine.transform);
        }
    }
    private float MathProcent(float point,float end)
    {
        float temp = 100f / end * point / 100f;
        return temp;
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
