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
    private List<string> musicTrackNames;

    private Dictionary<string, AudioClip> musicTracks = new Dictionary<string, AudioClip>();
    private AudioSource musicSource;
    private List<AudioSource> soundEffectsSources = new List<AudioSource>();

    private float musicVolume = 1f;
    private float soundEffectsVolume = 1f;


    private  string currentMusicName;
    public static AudioManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        instance = this;

        foreach (string name in musicTrackNames)
        {
            AddMusicTrack(name);
        }

        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicTracks.First().Value;

        SetMusicVolume(PlayerPrefs.GetFloat("musicVolume", 1f));
        SetSoundEffectVolume(PlayerPrefs.GetFloat("soundEffectsVolume", 1f));

        musicSource.Play();
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

    public void AddMusicTrack(string key)
    {
        AudioClip clip = Resources.Load<AudioClip>(key);
        musicTracks.Add(name, clip);
    }

    public void AddMusicTrack(string key, AudioClip musicTrack)
    {
        musicTrackNames.Add(key);
        musicTracks.Add(key, musicTrack);
    }

    public void RemoveMusicTrack(string key)
    {
        if (key == currentMusicName)
        {
            musicSource.Stop();
        }
        if (musicTrackNames.Contains(key))
        {
            musicTracks[key].UnloadAudioData();
        }
        musicTracks.Remove(key);
        musicTrackNames.Remove(key);
    }

    public void RemoveMusicTrack(AudioClip audioClip)
    {
        KeyValuePair<string, AudioClip> searchSource = musicTracks.FirstOrDefault(s => s.Value == audioClip);
        if (!searchSource.Equals(default(KeyValuePair<string, AudioClip>)))
        {
            RemoveMusicTrack(searchSource.Key);
        }
    }

    public void Play(string name)
    {
        if (musicTracks.ContainsKey(name))
        {
            musicSource.Stop();
            musicSource.clip = musicTracks[name];
            musicSource.Play();

        }
    }

    public AudioSource GetMusicSource()
    {
        return musicSource;
    }



    public void AddSoundEffectSource(AudioSource audioSource)
    {
        audioSource.volume = musicVolume;
        soundEffectsSources.Add(audioSource);
    }

    public void RemoveSoundEffectSource(AudioSource audioSource)
    {
        audioSource.Stop();
        soundEffectsSources.Remove(audioSource);
    }

}
