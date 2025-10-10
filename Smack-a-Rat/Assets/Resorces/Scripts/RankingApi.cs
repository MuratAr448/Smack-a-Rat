using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class RankingApi : MonoBehaviour
{
    public GetRankingApi getRankingApi;
    [SerializeField] private int RankNumber;
    [SerializeField] private TextMeshProUGUI Ranking;
    public string Rankuser;
    [SerializeField] private TextMeshProUGUI Name;
    public int Rankscore;
    [SerializeField] private TextMeshProUGUI Score;
    public int Rankduration;
    [SerializeField] private TextMeshProUGUI Time;
    private float delay = 0;
    private void Start()
    {
        getRankingApi = FindObjectOfType<GetRankingApi>();
        StartCoroutine(getRankingApi.GetRanking(RankNumber,this));
    }
    public void RankText()
    {
        Ranking.text = RankNumber + ".";
        Name.text = "Name: " + Rankuser;
        Score.text = "Score: " + Rankscore;
        Time.text = "Time: " + Minuut(Rankduration, 1) + ":" + Minuut(Rankduration, 2);
    }
    private int Minuut(int Duration,int MOS)
    {
        float time = Duration / 60;
        int mint = Mathf.FloorToInt(time);
        int sec = (int)(time - mint)*60; 
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
            return Duration;
        }
    }
}
