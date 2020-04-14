using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    //public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip coughSound;
    public AudioClip reloadSound;
    public AudioClip splashSound;
    public AudioClip shootSound;
    public AudioSource soundEffectAudio;
    public int a;  // flag for MainScene
    public int b;  // flag for GameMenu
    public bool vibration;

    void Start()
    {
        a = 0;
        b = 0;
        vibration = true;
        if (Instance == null)
        {
            Instance = this;    // makes sure this is the only SoundManager
        }
        else if (Instance != null)
        {
            Destroy(gameObject);    // if there are others, destroy them
        }

        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }
        
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            soundEffectAudio.clip = gameMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
        }
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            soundEffectAudio.clip = gameMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
        }

        /*
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            soundEffectAudio.mute = true;
        }
        else
        {
            soundEffectAudio.mute = false;
        }
        */

        DontDestroyOnLoad(gameObject.transform);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 1" && b == 0)
        {
            soundEffectAudio.clip = gameMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
            b++;
        }
        if (SceneManager.GetActiveScene().name == "MainMenu" && a == 0)
        {
            soundEffectAudio.clip = gameMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
            a++;
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

    public void PlayGameOverSound()
    {
        //soundEffectAudio.clip = paperRipSound;
        soundEffectAudio.loop = false;
        soundEffectAudio.Play(0);
    }

    public void PlayShotSound()
    {
        soundEffectAudio.clip = shootSound;
        soundEffectAudio.loop = false;
        soundEffectAudio.Play(0);
        StartCoroutine(ResumeGameplayMusic());
    }

    public void PlaySplashSound()
    {
        soundEffectAudio.clip = splashSound;
        soundEffectAudio.loop = false;
        soundEffectAudio.Play(0);
        StartCoroutine(ResumeGameplayMusic());
    }


    public void PlayReloadSound()
    {
        soundEffectAudio.clip = reloadSound;
        soundEffectAudio.loop = false;
        soundEffectAudio.Play(0);
        StartCoroutine(ResumeGameplayMusic());
    }

    public void PlayCoughSound()
    {
        soundEffectAudio.clip = coughSound;
        soundEffectAudio.loop = false;
        soundEffectAudio.Play(0);
        StartCoroutine( ResumeGameplayMusic() );
    }

    IEnumerator ResumeGameplayMusic()
    {
        yield return new WaitForSeconds(1f);
        soundEffectAudio.clip = gameMusic;
        soundEffectAudio.loop = true;
        soundEffectAudio.Play(0);
    }
}
