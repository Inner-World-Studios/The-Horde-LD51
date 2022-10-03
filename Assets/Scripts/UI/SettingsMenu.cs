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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            PauseManager.Resume();
        }
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OnQuitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

}
