using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : PoolableObject
{
    public float lifetime = 5f;
    public float damage = 1f;

    new ParticleSystem particleSystem;

    private float spawnTime;
    private bool hasHitTarget;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public override void OnCreate()
    {
        base.OnCreate();
        spawnTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - spawnTime) >= lifetime) {
            Disable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDisable)
        {
            GameObject target = collision.collider.gameObject;
            if (target.tag == "Player" || target.tag == "Enemy")
            {
                HealthController healthCon = target.GetComponent<HealthController>();
                if (target != null)
                {
                    FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                    joint.connectedBody = target.GetComponent<Rigidbody2D>();
                    joint.anchor = collision.contacts[0].point;

                    transform.parent = target.transform;

                    GetComponent<Rigidbody2D>().simulated = false;

                    healthCon.Damage(Random.Range(1, 31));
                    particleSystem.Play();

                }
                hasHitTarget = true;
            }
            else
            {
                Disable();
            }
        }
    }

}
