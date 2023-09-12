using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Pause pause;
    public void StartGame()
    {
        SceneManager.LoadScene("NewCarController");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Game");
    }
}