using UnityEngine;
using UnityEngine.SceneManagement;

public class UtiletyGame : MonoBehaviour
{
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ToStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
