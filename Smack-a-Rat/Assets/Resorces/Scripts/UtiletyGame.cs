using UnityEngine;
using UnityEngine.SceneManagement;

public class UtiletyGame : MonoBehaviour
{
    private CreateSessionApi CreateSessionApi;
    private void Start()
    {
        CreateSessionApi = GetComponent<CreateSessionApi>();
    }
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
    public void Quit()
    {
        Debug.Log("Quit");
        //Application.Quit();
    }
}
