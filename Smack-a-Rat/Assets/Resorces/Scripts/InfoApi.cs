using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InfoApi : MonoBehaviour 
{
    public static InfoApi Api;
    public string user_id;
    public int session_id;
    public int time_taken;
    public float accuracy;
    private void Awake()
    {
        if(Api == null)
        {
            Api = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    [Serializable]
    public class Score
    {
        public int score;
        public List<Times_Hit> Times_Hit;
    }
    [Serializable]
    public class Times_Hit
    {
        public bool Rat;
        public bool bombRat;
        public bool specialRat;
        public int time_Taken;
        public float got_Hit_Procent;
    }
}
