using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;

public class CreateSessionApi : MonoBehaviour
{
    [Serializable]
    private class CreateSessionRequest
    {
        public string username;
    }

    [Serializable]
    private class CreateSessionResponse
    {
        public bool ok;
        public long session_id;
        public string error;
    }

    private const string CreateSessionUrl = "http://localhost:5173/api/create-session";

    public IEnumerator CreateSession(string username)
    {
        var reqObj = new CreateSessionRequest { username = username };
        string json = JsonUtility.ToJson(reqObj);

        var www = new UnityWebRequest(CreateSessionUrl, "POST");
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
                var res = JsonUtility.FromJson<CreateSessionResponse>(text);
                if (res != null && res.ok)
                {
                    Debug.Log($"Session created. session_id = {res.session_id}");
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
        StartCoroutine(CreateSession("Jan"));
    }
}