using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    HealthController healthController;
    // Start is called before the first frame update
    void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.onDeath += OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
