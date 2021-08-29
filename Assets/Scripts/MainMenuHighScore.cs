using TMPro;
using UnityEngine;
public class MainMenuHighScore : MonoBehaviour
{
    public TMP_Text highScoretext;


    private void Start()
    {
        highScoretext.text = "HIGHSCORE - " + PlayerPrefs.GetInt("HighScore", 0);
    }


}
