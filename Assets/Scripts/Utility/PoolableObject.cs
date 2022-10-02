using UnityEngine;
using UnityEngine.Pool;

public class PoolableObject : MonoBehaviour
{
    public ObjectPool<PoolableObject> pool;

    public virtual void OnCreate()
    {

    }

    public virtual void Disable()
    {
        pool.Release(this);
    }
}