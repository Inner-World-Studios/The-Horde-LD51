using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private GameObject highscoreUI;

    private static ScoreManager instance;

    private GameObject scoreFloatTextSpawner;
    private long score;



    private void Awake()
    {
        instance = this;
        scoreFloatTextSpawner = highscoreUI.transform.Find("ScoreFloatTextSpawner").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static long GetScore()
    {
        return instance.score;
    }

    public static void AddScore(int score)
    {
        instance.score += score;
        string text = "Score: " + instance.score.ToString("N0");
        instance.highscoreUI.GetComponent<TextMeshProUGUI>().text = text;
        HealTextHandler.DisplayHeal(instance.scoreFloatTextSpawner, score, Color.yellow, Vector3.zero, HealText.Direction.DOWN);
    }
}
