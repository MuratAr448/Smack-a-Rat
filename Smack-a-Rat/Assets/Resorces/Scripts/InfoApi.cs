using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InfoApi : MonoBehaviour 
{
    public static InfoApi Api;
    public string userName;
    public int user_id;
    public int session_id;
    public int time_taken;
    public float accuracy;
    public int score;
    public List<int> ratKind;
    public List<int> time_Taken;
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
}
