using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject timerTextGobj;

    public delegate void OnLevelEvent();
    public static OnLevelEvent onLevelEvent;

    private TextMeshProUGUI timerText;

    private float startTimer;



    // Start is called before the first frame update
    void Start()
    {
        startTimer = 0;
        timerText = timerTextGobj.GetComponent<TextMeshProUGUI>();

    }


    // Update is called once per frame
    void Update()
    {
        startTimer += Time.deltaTime;
        timerText.text = "Next Event: "+ Mathf.FloorToInt(startTimer + 1).ToString();
        if (startTimer > 10f)
        {
            startTimer = 0;
            onLevelEvent?.Invoke();
        }
    }


    
    

}
