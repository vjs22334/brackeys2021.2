using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("references")]
    public SpawnSystem spawnSystem;

    [Header("GameSettings")]
    public int Totalives = 3;
    public int ScorePerLanding = 1;
    public int SCorePerKill = 1;

    [Header("UI References")]
    public TMP_Text ScoreText;
    public TMP_Text LivesText;

    public GameObject PauseUI;
    public GameObject GameOverUI;

    int lives;
    int score;

    public bool isPlaying;

    public int defenderRearmTime = 5;
    public int currentRearmTime = 5;
    public bool defenderSpawned = false;

    public List<SpriteRenderer> landingZoneIndicator;

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        lives = Totalives;
        score = 0;
        updateScoreUI();
        UpdatelivesUI();
        Time.timeScale = 1;
        isPlaying = true;
        ReArmDefender();
    }



    public void ReArmDefender()
    {
        defenderSpawned = false;
        StartCoroutine(RearmingDefender());
    }

    IEnumerator RearmingDefender()
    {
        while (!defenderSpawned)
        {
            if (currentRearmTime > 0)
            {
                yield return new WaitForSeconds(1f);
                currentRearmTime -= 1;
                //If you want to add UI to show countdown for defender
            }
            else
            {
                currentRearmTime = defenderRearmTime; ;
                spawnSystem.DefenderSpawn();
                defenderSpawned = true;
            }
        }
    }

    public void LandingZoneIndicator(LandZone landZone)
    {
        SpriteRenderer curretSpriteRen = null;
        switch (landZone)
        {
            case LandZone.LANDZONE_1:
                curretSpriteRen = landingZoneIndicator[0];
                break;
            case LandZone.LANDZONE_2:
                curretSpriteRen = landingZoneIndicator[1];
                break;
            case LandZone.LANDZONE_3:
                curretSpriteRen = landingZoneIndicator[2];
                break;
        }
        Sequence sequence = DOTween.Sequence();
        sequence.Append(curretSpriteRen.DOFade(0.1f, 0.3f));
        sequence.Append(curretSpriteRen.DOFade(0.9f, 0.3f));
        sequence.SetLoops<Sequence>(2, LoopType.Yoyo);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }

    void UpdatelivesUI()
    {
        LivesText.text = lives.ToString();
    }

    void updateScoreUI()
    {
        ScoreText.text = score.ToString();
    }

    public void AddScore(bool landing)
    {
        if (!isPlaying)
        {
            return;
        }
        if (landing)
        {
            score += ScorePerLanding;
        }
        else
        {
            score += SCorePerKill;
        }
        updateScoreUI();
    }

    public void RemoveLife()
    {
        if (lives == 0 || !isPlaying)
        {
            return;
        }
        lives--;
        UpdatelivesUI();
        if (lives == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isPlaying = false;
        GameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
}
