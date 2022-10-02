using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float damage = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.collider.gameObject;
        if (target.tag == "Player" || target.tag == "Enemy")
        {
            HealthController healthCon = target.GetComponent<HealthController>();
            if (target != null)
            {
                healthCon.Damage(Random.Range(1,3));
            }

        }

        Destroy(gameObject);
    }

}
