using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void PlayTheGame()
    {
        SceneManager.LoadScene("GameTest");
        AudioManager.Instance.ChangeMusic(CurrentScene.LEVEL1);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.ChangeMusic(CurrentScene.MAINMENU);
    }



}
