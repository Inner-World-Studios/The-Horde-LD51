using UnityEngine;
using UnityEngine.Pool;

public class PoolableObject : MonoBehaviour
{
    public ObjectPool<PoolableObject> pool;

    protected bool isDisable;

    public virtual void OnCreate()
    {
        isDisable = false;
    }

    public virtual void Disable()
    {
        if (!isDisable)
        {
            isDisable = true;
            pool.Release(this);
        }
    }
}