using UnityEngine;

public class GameMode : MonoBehaviour
{
    public GameType modeType;
    private static GameMode _instance = null;
    public static GameMode Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameMode>();
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
}
public enum GameType
{
    FRIENDLY,
    ENEMY,
}
