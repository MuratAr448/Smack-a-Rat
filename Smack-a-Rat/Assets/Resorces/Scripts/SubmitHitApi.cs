using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
public class SubmitHitApi : MonoBehaviour
{
    [Serializable]
    private class SubmitHitRequest
    {
        public int session_id;
        public int rat;
        public int whenHit;
    }
    [Serializable]
    private class SubmitScoreResponse
    {
        public bool ok;
        public string error;
    }
    private const string SubmitHitUrl = "http://localhost:5173/api/submit-hit";
    public IEnumerator SubmitHit(int sessionId, int rat, int whenHit)
    {
        SubmitHitRequest reqObj = new SubmitHitRequest
        {
            session_id = sessionId,
            rat = rat,
            whenHit = whenHit
        };

        string json = JsonUtility.ToJson(reqObj);

        UnityWebRequest www = new UnityWebRequest(SubmitHitUrl, "POST");
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
                SubmitScoreResponse res = JsonUtility.FromJson<SubmitScoreResponse>(text);
                if (res != null && res.ok)
                {
                    Debug.Log($"Saved Hit!");
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
                SubmitScoreResponse res = JsonUtility.FromJson<SubmitScoreResponse>(text);
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
