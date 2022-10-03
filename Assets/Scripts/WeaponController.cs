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

    public bool hasSplitShoot;

    private bool canAttack;

    private float lastAttackTime;

    private GameObject drawnArrow;

    private ObjectPool<PoolableObject> arrowPool;
    private GameObject poolParent;



    private void Awake()
    {
        arrowPool = new ObjectPool<PoolableObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 10, 1000);
        poolParent = GameObject.Find("World/Arrows");
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
        PoolableObject poolableObject = GameObject.Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity, poolParent.transform);
        poolableObject.pool = arrowPool;
        return poolableObject;
    }

    private void OnReturnedToPool(PoolableObject poolableObject)
    {
        poolableObject.gameObject.transform.SetParent(poolParent.transform);
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

    public void SetAttackTime(float time)
    {
        attackTime = Mathf.Clamp(time, 0.1f, 1f);
    }

    public float GetAttackTime()
    {
        return attackTime;
    }

    public void Attack()
    {
        if (CanAttack())
        {
            canAttack = false;
            lastAttackTime = Time.time;
            if (hasSplitShoot)
            {
                SpawnArrow(new Vector3(0, 0, -15f));
                SpawnArrow(new Vector3(0, 0, 15f));
                SpawnArrow(Vector3.zero);
            } else
            {
                SpawnArrow(Vector3.zero);
            }
            drawnArrow.SetActive(false);

            
        }

    }

    private ArrowController SpawnArrow(Vector3 rotation)
    {
        ArrowController arrow = arrowPool.Get() as ArrowController;
        arrow.transform.position = drawnArrow.transform.position;
        arrow.transform.rotation = drawnArrow.transform.rotation;
        Vector3 angles = arrow.transform.rotation.eulerAngles + rotation;
        arrow.transform.localRotation = Quaternion.Euler(angles);


        arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.rotation * Vector2.up * strength;
        arrow.damage = strength;

        return arrow;
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
