using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public enum MOB_TYPE { RED, TEAL, YELLOW, PINK};

    [SerializeField]
    private MOB_TYPE mobType;
    [SerializeField]
    private float strength;
    [SerializeField]
    private int scoreValue;


    private HealthController healthController;
    private new ParticleSystem particleSystem;

    private float lastDamageTime;


    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.onDeath += OnDeath;
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthController.IsDead() && !particleSystem.isEmitting)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<HealthController>().Damage(strength);
        }
    }

    private void OnDeath(GameObject gameObject)
    {
        particleSystem.Play();
        ScoreManager.AddScore(scoreValue);
    }
}
