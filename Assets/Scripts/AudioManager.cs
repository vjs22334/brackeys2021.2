using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip gameMusic;

    public List<AudioClip> soundEffects;

    private AudioSource bgMusicSource;
    private AudioSource soundEffecrSource;

    private static AudioManager _instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
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

        bgMusicSource = GetComponent<AudioSource>();
        soundEffecrSource = GetComponentInChildren<AudioSource>();
        SetVolume(PlayerPrefs.GetFloat("Volume"));

    }

    public void ChangeVolume()
    {
        float volume = transform.Find("VolumeSlider").GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("Volume", volume);
        SetVolume(volume);
    }

    private void SetVolume(float vol)
    {
        bgMusicSource.volume = vol;
        soundEffecrSource.volume = vol;
    }

    public void ChangeMusic(CurrentScene current)
    {
        switch (current)
        {
            case CurrentScene.MAINMENU:
                bgMusicSource.clip = backgroundMusic;
                break;
            case CurrentScene.LEVEL1:
                bgMusicSource.clip = gameMusic;
                break;
        }
    }

    public void PlayTheSoundEffect(TypesOfSoundEffect typesOfSoundEffect)
    {
        switch (typesOfSoundEffect)
        {
            case TypesOfSoundEffect.GAMESTART:
                soundEffecrSource.clip = soundEffects[0];
                break;

            case TypesOfSoundEffect.GAMEOVER:
                soundEffecrSource.clip = soundEffects[1];
                break;
            case TypesOfSoundEffect.SHIPDESTROY:
                soundEffecrSource.clip = soundEffects[2];
                break;

            case TypesOfSoundEffect.SHIPLANDED:
                soundEffecrSource.clip = soundEffects[3];
                break;
            case TypesOfSoundEffect.COMET:
                soundEffecrSource.clip = soundEffects[4];
                break;

        }
    }



}

public enum CurrentScene
{
    MAINMENU,
    LEVEL1,
}

public enum TypesOfSoundEffect
{
    GAMESTART,
    GAMEOVER,
    SHIPDESTROY,
    SHIPLANDED,
    COMET,

}
