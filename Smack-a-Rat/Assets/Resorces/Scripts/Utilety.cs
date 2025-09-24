using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Utilety : MonoBehaviour
{
    public GameObject start;
    public GameObject leaderBoard;
    public GameObject startName;
    private void Start()
    {
        string info = FindObjectOfType<InfoApi>().userName;
        if (info != "")
        {
            ToStart();
        }else
        {
            ToStartName();
        }
    }
    public void ToStart()
    {
        start.SetActive(true);
        startName.SetActive(false);
        leaderBoard.SetActive(false);
    }
    public void ToLeaderboard()
    {
        start.SetActive(false);
        startName.SetActive(false);
        leaderBoard.SetActive(true);
    }
    public void ToStartName()
    {
        start.SetActive(false);
        startName.SetActive(true);
        leaderBoard.SetActive(false);
    }
}
