using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine.Windows;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField Name;
    [SerializeField] private CreateSessionApi CreateSessionApi;
    public bool alow = false;
    private void GrabName(string input)
    {
        if (input == "")
        {
            alow = false;// get name api
        }
        else
        {
            alow = true;
        }
       
    }
    public void GotNameCheck()
    {
        //GrabName(Name.text);
        CreateSessionApi.CreateSession(Name.text);
    }
}
