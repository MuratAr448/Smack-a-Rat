using UnityEngine;
using UnityEngine.SceneManagement;
public class Utilety : MonoBehaviour
{
    public GameObject start;
    public GameObject leaderBoard;
    public GameObject startName;
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
