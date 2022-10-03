using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float health;

    [SerializeField]
    private GameObject healthBarUI;

    [SerializeField]
    private bool healthBarTrack;

    [SerializeField]
    private Vector3 healthBarPositionOffset;

    public delegate void OnDeath();
    public OnDeath onDeath;

    public delegate void OnHealthChange();
    public OnHealthChange onHealthChange;

    private GameObject healthBarsCanvas;

    private void Awake()
    {
        healthBarsCanvas = GameObject.Find("Canvas/HealthBars");
        if (Utilities.IsPrefab(healthBarUI))
        {
            healthBarUI = GameObject.Instantiate(healthBarUI, healthBarsCanvas.transform);
        }
        healthBarUI.transform.parent = healthBarsCanvas.transform;
        onHealthChange += HealthChange;
        HealthChange();

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
        onHealthChange?.Invoke();

        HealTextHandler.DisplayHeal(transform, heal, Color.green);
    }

    public void Damage(float damage)
    {
        health -= damage;
        onHealthChange?.Invoke();

        if (health < 0)
        {
            onDeath?.Invoke();
        }

        HealTextHandler.DisplayHeal(transform, -damage, Color.red);
    }

    private void HealthChange()
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
