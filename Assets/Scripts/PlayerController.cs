using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float cameraFollowSpeed;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotateSpeed;

    private new Rigidbody2D rigidbody2D;

    private WeaponController weaponController;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        weaponController = transform.Find("Weapon").GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!PauseManager.IsPaused())
        {
            HandleInput();
        }
    }

    void HandleInput()
    {

        //handle player movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody2D.velocity = new Vector2(horizontal, vertical) * speed;

        Vector3 pos = transform.position;
        pos.z = Camera.main.transform.position.z;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, Time.deltaTime * cameraFollowSpeed);

        //handle player aim
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 diff = mouseWorldPos - transform.position;
        diff.Normalize();

        float oldAngle = transform.rotation.eulerAngles.z;
        float angle = (Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg) - 90;


        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotateSpeed);

        if (Input.GetMouseButton(0))
        {
            weaponController.Attack();
        }

    }

}
