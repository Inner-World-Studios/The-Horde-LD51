using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public enum Events : int
    {
        BOSS,
        SPLIT_SHOT,
        ATTACK_UP,
        ATTACK_DOWN,
        HEAL
    }
    [SerializeField]
    private float eventTimer = 10f;

    [SerializeField]
    private GameObject timerTextGobj;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject bossSpawner;
    [SerializeField]
    private GameObject notification;



    public delegate void OnLevelEvent();
    public static OnLevelEvent onLevelEvent;

    private TextMeshProUGUI timerText;

    private float startTimer;

    private List<GameObject> bosses = new List<GameObject>();

    private CanvasGroup notificationCanvasGroup;
    private TextMeshProUGUI notificationText;

    private float lastUpdateTime;


    // Start is called before the first frame update
    void Start()
    {
        startTimer = 0;
        timerText = timerTextGobj.GetComponent<TextMeshProUGUI>();
        notificationCanvasGroup = notification.GetComponent<CanvasGroup>();
        notificationText = notification.transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {

        startTimer += Time.deltaTime;
        timerText.text = "Next Event: "+ Mathf.FloorToInt(startTimer + 1).ToString();
        if (startTimer >= eventTimer)
        {
            startTimer = 0;
            LevelEvent();
        }

        if ((Time.time - lastUpdateTime) >= 0.001f)
        {
            lastUpdateTime = Time.time;
            notificationCanvasGroup.alpha -= 0.0005f;
        }
    }

    public void LevelEvent()
    {
        int totalEvents = Enum.GetNames(typeof(Events)).Length;
        int rand = UnityEngine.Random.Range(0, totalEvents);

        Events e = (Events)rand;

        float r = UnityEngine.Random.value;
        WeaponController wc = player.transform.Find("Weapon").GetComponent<WeaponController>();
        switch (e)
        {
            case Events.BOSS:
                GameObject boss = bossSpawner.GetComponent<MobSpawner>().SpawnMob();
                boss.GetComponent<HealthController>().onDeath += OnBossDeath;
                bosses.Add(boss);
                AudioManager.GetInstance().Play("boss_theme");
                notificationText.text = "A boss has entered the horde!";
                notificationText.color = Color.red;
                break;
            case Events.SPLIT_SHOT:
                wc.hasSplitShoot = !wc.hasSplitShoot;
                if (wc.hasSplitShoot)
                {
                    notificationText.text = "You have received Split shot!";
                    notificationText.color = Color.green;
                } else
                {
                    notificationText.text = "You have lost Split shot!";
                    notificationText.color = Color.red;
                }
                break;

            case Events.ATTACK_UP:
                r /= 10;
                wc.SetAttackTime(wc.GetAttackTime() - r);
                notificationText.text = "Attack speed up!";
                notificationText.color = Color.green;
                break;

            case Events.ATTACK_DOWN:
                r /= 10;
                wc.SetAttackTime(wc.GetAttackTime() + r);
                notificationText.text = "Attack speed down!";
                notificationText.color = Color.red;

                break;
            case Events.HEAL:
                float heal = UnityEngine.Random.Range(10, 50);
                player.GetComponent<HealthController>().Heal(heal);
                notificationText.text = "You have been healed!";
                notificationText.color = Color.green;
              break;
        }

        notificationCanvasGroup.alpha = 1f;

        onLevelEvent?.Invoke();
    }

    private void OnBossDeath(GameObject gameObject)
    {
        bosses.Remove(gameObject);
        if (bosses.Count == 0)
        {
            AudioManager.GetInstance().Play("battle_theme");
        }
    }



}
