using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void PlayTheFriendlyMode()
    {
        SceneManager.LoadScene("GameTest");
    }

    public void PlayTheDefendingMode()
    {
        SceneManager.LoadScene("GameTest");//Change the scene name here
    }


    public void QuitTheGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.ChangeMusic(CurrentScene.MAINMENU);
    }



}
