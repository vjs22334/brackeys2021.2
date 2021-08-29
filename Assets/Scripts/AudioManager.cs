using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    //public AudioClip gameMusic;

    public List<AudioClip> soundEffects;

    public AudioSource bgMusicSource;
    public AudioSource soundEffecrSource;

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
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

        ChangeMusic(CurrentScene.MAINMENU);
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.5f));
        SetSoundVolume(PlayerPrefs.GetFloat("SoundVolume", 0.5f));

    }

    public void ChangeTheMusicVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
        SetMusicVolume(slider.value);
    }
    public void ChangeTheSoundVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("SoundVolume", slider.value);
        SetSoundVolume(slider.value);
    }

    public void StopSoundEffect()
    {
        soundEffecrSource.Stop();
    }


    private void SetMusicVolume(float musicVolume)
    {
        bgMusicSource.volume = musicVolume;

    }

    private void SetSoundVolume(float soundEffectVolume)
    {
        soundEffecrSource.volume = soundEffectVolume;
    }


    public void ChangeMusic(CurrentScene current)
    {
        bgMusicSource.clip = backgroundMusic;
        bgMusicSource.Play();
    }

    public void PlayTheSoundEffect(TypesOfSoundEffect typesOfSoundEffect)
    {
        switch (typesOfSoundEffect)
        {
            case TypesOfSoundEffect.GAMESTART:
                soundEffecrSource.PlayOneShot(soundEffects[0]);
                break;

            case TypesOfSoundEffect.GAMEOVER:
                soundEffecrSource.PlayOneShot(soundEffects[1]);
                break;
            case TypesOfSoundEffect.SHIPDESTROY:
                soundEffecrSource.PlayOneShot(soundEffects[2]);
                break;

            case TypesOfSoundEffect.SHIPLANDED:
                soundEffecrSource.PlayOneShot(soundEffects[3]);
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
