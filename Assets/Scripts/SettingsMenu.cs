using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider soundEffectsVolumeSlider;

    private bool isPaused;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenu == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    Pause();
                } else
                {
                    Resume();
                }
            }
        }

        if (isActiveAndEnabled)
        {
            musicVolumeSlider.value = AudioManager.GetInstance().GetMusicVolume();
            soundEffectsVolumeSlider.value = AudioManager.GetInstance().GetSoundEffectsVolume();
        }
    }

    public void OnMusicVolumeChange()
    {
        AudioManager.GetInstance().SetMusicVolume(musicVolumeSlider.value);
    }
    public void OnSoundEffectVolumeChange()
    {
        AudioManager.GetInstance().SetSoundEffectVolume(soundEffectsVolumeSlider.value);
    }

    public void OnBackButtonClick()
    {
        if (mainMenu != null)
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
        } else
        {
            Resume();
        }

    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        gameObject.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        gameObject.SetActive(false);
    }
}
