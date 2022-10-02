using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnSettingsButtonClick()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnExitButtonClick()
    {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
