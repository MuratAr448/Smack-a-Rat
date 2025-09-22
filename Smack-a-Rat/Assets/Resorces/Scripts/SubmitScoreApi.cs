using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;

public class SubmitScoreApi : MonoBehaviour
{
    [Serializable]
    private class SubmitScoreRequest
    {
        public long session_id;
        public long level_id;
        public int score;
        public int time_taken;
        public float accuracy;
    }

    [Serializable]
    private class SubmitScoreResponse
    {
        public bool ok;
        public long score_id;
        public string error;
    }

    private const string SubmitScoreUrl = "http://localhost:5173/api/submit-score";

    public IEnumerator SubmitScore(long sessionId, long levelId, int score, int timeTaken, float accuracy)
    {
        SubmitScoreRequest reqObj = new SubmitScoreRequest
        {
            session_id = sessionId,
            level_id = levelId,
            score = score,
            time_taken = timeTaken,
            accuracy = accuracy
        };

        string json = JsonUtility.ToJson(reqObj);

        UnityWebRequest www = new UnityWebRequest(SubmitScoreUrl, "POST");
        www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            var text = www.downloadHandler.text;
            try
            {
                var res = JsonUtility.FromJson<SubmitScoreResponse>(text);
                if (res != null && res.ok)
                {
                    Debug.Log($"Score saved. score_id = {res.score_id}");
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
        }
    }

    private void Start()
    {
        long sessionId = 3;
        long levelId = 1;
        StartCoroutine(SubmitScore(sessionId, levelId, 2552, 72, 64.2f));
    }
}
