using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : PoolableObject
{
    public float lifetime = 5f;
    public float damage = 1f;

    private new ParticleSystem particleSystem;
    private AudioSource hitSoundSource;

    private float spawnTime;
    private bool hasHitTarget;


    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        hitSoundSource = GetComponent<AudioSource>();
        AudioManager.GetInstance().AddSoundEffectSource(hitSoundSource);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        spawnTime = Time.time;
        hasHitTarget = false;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
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

        if (!isDisable && hasHitTarget && transform.parent == null)
        {
            Disable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDisable && !hasHitTarget)
        {


            GameObject target = collision.collider.gameObject;
            if (target.GetComponent<ArrowController>() != null)
            {
                return;
            }

            hitSoundSource.Play();
            if (target.tag == "Player" || target.tag == "Enemy")
            {
                HealthController healthCon = target.GetComponent<HealthController>();
                if (target != null)
                {
                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<Rigidbody2D>().simulated = false;

                    transform.SetParent(target.transform);

                    healthCon.Damage(Random.Range(1, 10) * damage);
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

    public void OnDestroy()
    {
        AudioManager.GetInstance().RemoveSoundEffectSource(hitSoundSource);
    }

}
