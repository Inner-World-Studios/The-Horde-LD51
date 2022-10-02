using UnityEngine;
using UnityEngine.Pool;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private ArrowController arrowPrefab;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float attackTimeLength;
    [SerializeField]
    private float strength;

    private bool canAttack;

    private float lastAttackTime;

    private GameObject drawnArrow;

    private ObjectPool<PoolableObject> arrowPool;



    private void Awake()
    {
        arrowPool = new ObjectPool<PoolableObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 5, 10);
    }

    // Start is called before the first frame update
    void Start()
    {
        drawnArrow = transform.Find("Arrow").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!drawnArrow.activeSelf && CanAttack())
        {
            drawnArrow.SetActive(true);
        }

    }

    private PoolableObject CreatePooledItem()
    {
        PoolableObject poolableObject = GameObject.Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("World/Arrows").transform);
        poolableObject.pool = arrowPool;
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
        Destroy(poolableObject.GetComponent<Joint2D>());
        poolableObject.GetComponent<Rigidbody2D>().simulated = true;
    }

    private void OnDestroyPoolObject(PoolableObject poolableObject)
    {
        Destroy(poolableObject);
    }


    public void Attack()
    {
        if (CanAttack())
        {
            canAttack = false;
            lastAttackTime = Time.time;
            PoolableObject arrow = arrowPool.Get();
            arrow.transform.position = drawnArrow.transform.position;
            arrow.transform.rotation = drawnArrow.transform.rotation;
            arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.rotation * Vector2.up * strength;
            drawnArrow.SetActive(false);
        }

    }

    public bool CanAttack()
    {
        if (!canAttack && (Time.time - lastAttackTime) > attackTime)
        {
            canAttack = true;
        }

        return canAttack;
    }
}
