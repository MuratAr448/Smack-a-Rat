using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using static RankingApi;

public class RankingApi : MonoBehaviour
{
    public enum Surche
    {
        firstThree,
        yourStanding,
        thisSession
    }
    public Surche surche;
    private InfoApi info;
    public GetCurrentRankingApi getCurrentRankingApi;
    public GetRankingApi getRankingApi;
    [SerializeField] private int RankNumber;
    [SerializeField] private TextMeshProUGUI Ranking;
    public string Rankuser;
    [SerializeField] private TextMeshProUGUI Name;
    public int Rankscore;
    [SerializeField] private TextMeshProUGUI Score;
    public int Rankduration;
    [SerializeField] private TextMeshProUGUI Time;
    private float delay = 0.3f;
    private void Start()
    {
        getRankingApi = FindObjectOfType<GetRankingApi>();
        getCurrentRankingApi = FindObjectOfType<GetCurrentRankingApi>();
        info = FindObjectOfType<InfoApi>();
        switch (surche)
        {
            case Surche.firstThree:
                StartCoroutine(getRankingApi.GetRanking(RankNumber, this));
                break;
            case Surche.yourStanding:
                StartCoroutine(getCurrentRankingApi.GetCurrentRanking(info.user_id, this));
                break;
            case Surche.thisSession:
                StartCoroutine(ThisSession());

                break;
            default: break;
        }
        
    }
    private IEnumerator ThisSession()
    {
        yield return new WaitForSeconds(delay);
        Ranking.text = "";
        Name.text = "" + info.userName;
        Score.text = "Score: " + info.score;
        Time.text = Minuut(info.time_taken, 1) + ":" + Minuut(info.time_taken, 2)+ " :Time";
    }
    public void RankText()
    {
        Ranking.text = RankNumber + ".";
        Name.text = "Name: " + Rankuser;
        Score.text = "Score: " + Rankscore;
        Time.text = "Time: " + Minuut(Rankduration, 1) + ":" + Minuut(Rankduration, 2);
    }
    private int Minuut(float Duration,int MOS)
    {
        float time = (Duration / 60);
        int mint = Mathf.FloorToInt(time);
        float temp = (time - mint)*60;
        int sec = (int)temp;
        if (MOS == 1)
        {
            return mint;
        }
        else if (MOS == 2)
        {
            return sec;
        }
        else
        {
            return (int)Duration;
        }
    }
}
