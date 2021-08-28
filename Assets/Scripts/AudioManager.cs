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
        SetVolume(PlayerPrefs.GetFloat("Volume", 0.5f));

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
