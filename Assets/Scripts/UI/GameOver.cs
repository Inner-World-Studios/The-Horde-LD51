using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject gameoverScreen;
    [SerializeField]
    public GameObject finalScoreText;

    private static GameOver instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DisplayGameover()
    {
        GameObject.Find("Canvas/Game UI").SetActive(false);
        instance.gameoverScreen.SetActive(true);
        string text = "Final Score:\n" + ScoreManager.GetScore();
        instance.finalScoreText.GetComponent<TextMeshProUGUI>().text = text;
        Time.timeScale = 0;
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }
    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
}
