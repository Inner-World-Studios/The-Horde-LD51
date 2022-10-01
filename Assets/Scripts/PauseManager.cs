using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;

    private bool isPaused;

    private static PauseManager instance;

    public static PauseManager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public static bool IsPaused()
    {
        return instance.isPaused;
    }

    public static void Pause()
    {
        instance.isPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        instance.settingsMenu.SetActive(true);
    }

    public static void Resume()
    {
        instance.isPaused = false;
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        instance.settingsMenu.SetActive(false);
    }
}
