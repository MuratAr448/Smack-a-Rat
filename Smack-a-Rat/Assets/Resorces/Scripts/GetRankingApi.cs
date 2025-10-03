using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GetRankingApi : MonoBehaviour
{
    [Serializable]
    private class GetRankingRequest
    {
        public int rank_number;
    }
    [Serializable]
    private class GetRankingResponse
    {
        public bool ok;
        public string user;
        public int score;
        public int duration;
        public string error;
    }
    private const string GetRankingUrl = "http://localhost:5173/api/get-ranking";

    public IEnumerator GetRanking(int rank_number, RankingApi ranking)
    {
        GetRankingRequest reqObj = new GetRankingRequest { rank_number = rank_number };
        string json = JsonUtility.ToJson(reqObj);

        UnityWebRequest www = new UnityWebRequest(GetRankingUrl, "POST");//split for only name
        www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string text = www.downloadHandler.text;
            try
            {
                GetRankingResponse res = JsonUtility.FromJson<GetRankingResponse>(text);
                if (res != null && res.ok)
                {
                    Debug.Log($"user good. name = {res.user}");
                    ranking.Rankuser = res.user;
                    ranking.Rankscore = res.score;
                    ranking.Rankduration = res.duration;

                }
                else
                {
                    Debug.LogError($"Server did not return ok. Response: {text}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to parse JSON: {e.Message}\nResponse: {text}");
            }
        }
        else
        {
            Debug.LogError($"HTTP error: {www.responseCode} - {www.error}");
            try
            {
                string text = www.downloadHandler.text;
                GetRankingResponse res = JsonUtility.FromJson<GetRankingResponse>(text);
                if (res != null && !string.IsNullOrEmpty(res.error))
                {
                    Debug.LogError($"The server provided the following error message: {res.error}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to parse JSON: {e.Message}");
            }
        }
    }
}
