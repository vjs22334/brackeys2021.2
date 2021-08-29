using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{


    public void PlayTheFriendlyMode()
    {
        SceneManager.LoadScene("FriendlyMode");
        GameMode.Instance.modeType = GameType.FRIENDLY;
    }

    public void PlayTheDefendingMode()
    {
        SceneManager.LoadScene("GameTest");//Change the scene name here
        GameMode.Instance.modeType = GameType.ENEMY;
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
        AudioManager.Instance.StopSoundEffect();
        GameManager.Instance.CheckHighScoreAndUpdate();
        SceneManager.LoadScene("MainMenu");

    }



}


