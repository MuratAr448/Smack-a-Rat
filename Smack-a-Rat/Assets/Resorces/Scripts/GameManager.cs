using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Rats;
    public List<GameObject> Locations;
    public int lives = 3;
    [SerializeField] private TextMeshProUGUI healthText;
    public float ratActiveSpeed = 1.0f;
    private float ratSpawnSpeed = 3f;
    private float timer = 0f;
    private float totalTime = 0f;
    private List<float> TimesLost = new List<float>();
    void Start()
    {
        
    }

    void Update()
    {
        healthText.text = "Lives: " + lives;
        if(timer > ratSpawnSpeed)
        {
            timer = 0f;
            SpawnWitchRat();
        }
        if (lives > 0)
        {
            timer += Time.deltaTime;
            totalTime += Time.deltaTime;
        }
    }

    private void SpawnWitchRat()
    {
        int rand = Random.Range(0, 11);
        
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
            int rand = Random.Range(0, LeftLocations.Count);
            return LeftLocations[rand]; 
        }else
        {
            return -1;
        }
    }
    public void LostLifeTime()
    {
        TimesLost.Add(totalTime);
    }
}
