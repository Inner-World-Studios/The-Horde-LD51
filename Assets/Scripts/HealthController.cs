using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    public float health;
    [SerializeField]
    private GameObject healthBarUI;
    [SerializeField]
    private bool healthBarTrack;
    [SerializeField]
    private Vector3 healthBarPositionOffset;
    [SerializeField]
    private bool hasImmunityFrames;
    [SerializeField]
    private float immunityFrameTime;

    private float lastDamageTime;

    public delegate void OnDeath(GameObject gameObject);
    public OnDeath onDeath;

    public delegate void OnHealthChange(GameObject gameObject);
    public OnHealthChange onHealthChange;

    private GameObject healthBarsCanvas;

    private bool isDead;

    private void Awake()
    {
        healthBarsCanvas = GameObject.Find("Canvas/Game UI/HealthBars");
        if (Utilities.IsPrefab(healthBarUI))
        {
            healthBarUI = GameObject.Instantiate(healthBarUI, healthBarsCanvas.transform);
        }
        healthBarUI.transform.SetParent(healthBarsCanvas.transform);
        onHealthChange += HealthChange;
        HealthChange(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarTrack)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            healthBarUI.transform.position = pos + healthBarPositionOffset;
        }
    }

    public void Heal(float heal)
    {
        health = Mathf.Clamp(health + heal, 0, maxHealth);
        onHealthChange?.Invoke(gameObject);

        HealTextHandler.DisplayHeal(gameObject, heal, Color.green);
    }

    public void Damage(float damage)
    {
        if (CanReceiveDamage())
        {
            lastDamageTime = Time.time;
            health -= damage;
            onHealthChange?.Invoke(gameObject);

            if (health <= 0)
            {
                Death();
            }

            HealTextHandler.DisplayHeal(gameObject, -damage, Color.red);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool CanReceiveDamage()
    {
        return !hasImmunityFrames || (Time.time - lastDamageTime) > immunityFrameTime;
    }


    private void Death()
    {
        isDead = true;
        healthBarUI.SetActive(false);

        onDeath?.Invoke(gameObject);
    }

    private void HealthChange(GameObject gameObject)
    {
        RectTransform bar = healthBarUI.transform.Find("Bar").GetComponent<RectTransform>();
        TextMeshProUGUI text = healthBarUI.transform.Find("HealthText").GetComponent<TextMeshProUGUI>();

        float healthPercent = (health / maxHealth) * 100f;
        bar.sizeDelta = new Vector2(Mathf.Ceil(healthPercent), bar.sizeDelta.y);
        text.text = health.ToString("N0") + "/" + maxHealth.ToString("N0");
    }

    private void OnDestroy()
    {
        Destroy(healthBarUI);
    }
}
