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
        if (CanSpawn())
        {
            SpawnMob();
        }
    }

    private bool CanSpawn()
    {
        return (Time.time - lastSpawnTime) >= spawnRate;
    }

    private GameObject SpawnMob()
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

        lastSpawnTime = Time.time;

        return mob;
    }
}
