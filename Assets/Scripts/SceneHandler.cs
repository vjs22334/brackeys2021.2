using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public ModeType modeType;


    private static SceneHandler _instance = null;
    public static SceneHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneHandler>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    public void PlayTheFriendlyMode()
    {
        SceneManager.LoadScene("FriendlyMode");
        modeType = ModeType.FRIENDLY;
    }

    public void PlayTheDefendingMode()
    {
        SceneManager.LoadScene("GameTest");//Change the scene name here
        modeType = ModeType.ENEMY;
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

    }



}

public enum ModeType
{
    FRIENDLY,
    ENEMY,
}
