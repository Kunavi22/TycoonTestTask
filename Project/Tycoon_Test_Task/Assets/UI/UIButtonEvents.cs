using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonEvents : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SaveManager.instance.SaveGameState();
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        DataSaver.ClearAllData();

        SceneManager.LoadScene("MainGame");
    }

    public void Continue()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
