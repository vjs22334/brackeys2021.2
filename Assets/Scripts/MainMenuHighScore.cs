using TMPro;
using UnityEngine;
public class MainMenuHighScore : MonoBehaviour
{
    public TMP_Text normalModeScoretext;
    public TMP_Text defenderModeScoretext;


    private void Start()
    {
        normalModeScoretext.text = "HOMEBASE - " + PlayerPrefs.GetInt("FriendlyMode", 0);
        defenderModeScoretext.text = "FRONTLINE - " + PlayerPrefs.GetInt("EnemyMode", 0);

    }


}
