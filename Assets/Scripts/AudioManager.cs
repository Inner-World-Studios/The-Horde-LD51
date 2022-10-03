using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField]
    private List<AudioClip> musicTracks = new List<AudioClip>();

    private AudioSource musicSource;
    private List<AudioSource> soundEffectsSources = new List<AudioSource>();

    private float musicVolume = 0.5f;
    private float soundEffectsVolume = 0.75f;


    private  string currentMusicName;
    public static AudioManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        instance = this;

        musicSource = GetComponent<AudioSource>();

        SetMusicVolume(PlayerPrefs.GetFloat("musicVolume", 1f));
        SetSoundEffectVolume(PlayerPrefs.GetFloat("soundEffectsVolume", 1f));

        if (musicTracks.Count != 0)
        {
            musicSource.clip = musicTracks.First();
            musicSource.Play();
        }
    }

    void Update()
    {
        
    }

    public void SetMusicVolume(float musicVolume)
    {
        this.musicVolume = Mathf.Clamp(musicVolume,0,1);
        PlayerPrefs.SetFloat("musicVolume", this.musicVolume);

        musicSource.volume = this.musicVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public void SetSoundEffectVolume(float soundEffectsVolume)
    {
        this.soundEffectsVolume = Mathf.Clamp(soundEffectsVolume, 0, 1);
        PlayerPrefs.SetFloat("soundEffectsVolume", this.soundEffectsVolume);

        foreach (AudioSource source in soundEffectsSources)
        {
            source.volume = this.soundEffectsVolume;
        }
    }

    public float GetSoundEffectsVolume()
    {
        return soundEffectsVolume;
    }


    public void Play(string name)
    {
        AudioClip clip = musicTracks.FirstOrDefault<AudioClip>(c => c.name == name);
        if (clip != null && musicSource.clip.name != clip.name)
        {
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();

        }
    }

    public AudioSource GetMusicSource()
    {
        return musicSource;
    }



    public void AddSoundEffectSource(AudioSource audioSource)
    {
        audioSource.volume = soundEffectsVolume;
        soundEffectsSources.Add(audioSource);
    }

    public void RemoveSoundEffectSource(AudioSource audioSource)
    {
        audioSource.Stop();
        soundEffectsSources.Remove(audioSource);
    }

}
