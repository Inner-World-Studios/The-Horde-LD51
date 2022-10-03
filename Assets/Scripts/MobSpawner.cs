using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> mobPrefabs;
    [SerializeField]
    private List<float> spawnTypeChance;
    [SerializeField]
    private float spawnRate;

    [SerializeField]
    private bool isBoss;

    private float lastSpawnTime;

    private GameObject playerTarget;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBoss && CanSpawn())
        {
            SpawnMob();
        }
    }

    private bool CanSpawn()
    {
        return (Time.time - lastSpawnTime) >= spawnRate;
    }

    public GameObject SpawnMob()
    {
        float rand = Random.value;
        int index = 0;
        for (; index < spawnTypeChance.Count; index++) { 
            if (rand >= spawnTypeChance[index])
            {
                break;
            } 
        }
        GameObject mob = Instantiate(mobPrefabs[index], transform);
        mob.GetComponent<AIDestinationSetter>().target = playerTarget.transform;
        if (isBoss)
        {
            mob.transform.localScale *= 4;
            HealthController hc = mob.GetComponent<HealthController>();
            hc.maxHealth *= 10;
            hc.health *= 10;
        }
        lastSpawnTime = Time.time;

        return mob;
    }
}
