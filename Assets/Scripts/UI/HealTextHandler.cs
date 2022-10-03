using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HealTextHandler : MonoBehaviour
{
    [SerializeField]
    private HealText healTextPrefab;

    private static HealTextHandler instance;
    private ObjectPool<PoolableObject> healTextPool;

    private void Awake()
    {
        instance = this;
        healTextPool = new ObjectPool<PoolableObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 10, 100);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private PoolableObject CreatePooledItem()
    {
        PoolableObject poolableObject = GameObject.Instantiate(healTextPrefab, Vector3.zero, Quaternion.identity, transform);
        poolableObject.pool = healTextPool;
        return poolableObject;
    }

    private void OnReturnedToPool(PoolableObject poolableObject)
    {
        poolableObject.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(PoolableObject poolableObject)
    {
        poolableObject.OnCreate();
        poolableObject.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(PoolableObject poolableObject)
    {
        Destroy(poolableObject);
    }

    public static void DisplayHeal(GameObject owner, float heal)
    {
        DisplayHeal(owner, heal, Color.white);
    }

    public static void DisplayHeal(GameObject owner, float heal, Color color)
    {
        DisplayHeal(owner, heal, color, Vector3.zero, HealText.Direction.UP);
    }

    public static void DisplayHeal(GameObject owner, float heal, Color color, Vector3 offset, HealText.Direction direction)
    {
        PoolableObject poolableObject = instance.healTextPool.Get();
        if (poolableObject != null)
        {
            HealText healText = poolableObject.GetComponent<HealText>();
            healText.transform.SetParent(instance.transform);
            healText.SetOwner(owner);
            healText.text.SetText(heal.ToString("N0"));
            healText.text.color = color;
            healText.direction = direction;
        }
    }


}
