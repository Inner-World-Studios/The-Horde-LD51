using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float health;

    public delegate void OnDeath();
    public OnDeath onDeath;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal(float heal)
    {
        health = Mathf.Clamp(health + heal, 0, maxHealth);
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            onDeath?.Invoke();
        }

        HealTextHandler.DisplayHeal(transform, -damage, Color.red);
    }


}
