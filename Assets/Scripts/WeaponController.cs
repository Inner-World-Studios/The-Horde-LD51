using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float attackTimeLength;
    [SerializeField]
    private float strength;

    private bool canAttack;

    private float lastAttackTime;

    private GameObject drawnArrow;

    private GameObject arrowsGroupObj;

    


    // Start is called before the first frame update
    void Start()
    {
        arrowsGroupObj = GameObject.Find("Arrows");
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


    public void Attack()
    {
        if (CanAttack())
        {
            canAttack = false;
            lastAttackTime = Time.time;

            GameObject arrow = Instantiate(arrowPrefab, drawnArrow.transform.position, drawnArrow.transform.rotation, arrowsGroupObj.transform);
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
