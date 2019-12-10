using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundMovement : MonoBehaviour
{

    public float MoveSpeed = 8f;
    public Rigidbody PlayerRB;

    [SerializeField]
    float RotationSpeed;
    private GameObject bulletplaceHolder;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float bulletSpeed;

    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    public GameObject groundplane;
    float rayLength;

    public Image crosshair;

    Vector3 Movement;
    Vector3 worldMouse;

    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
        bulletplaceHolder = GameObject.FindGameObjectWithTag("BPlaceHolder");
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Shooting();

        Movement.x = Input.GetAxis("Horizontal");
        Movement.z = Input.GetAxis("Vertical");
        var mouse = Input.mousePosition;
        mouse.z = 0;

        crosshair.transform.position = mouse;

        var ScreenPlayer = Camera.main.WorldToScreenPoint(this.transform.position);
        ScreenPlayer.z = 0;


        var dir = (mouse - ScreenPlayer).normalized;
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.black);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
        dir.z = dir.y;
        dir.y = 0;
        transform.forward = dir;
    }

    private void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + Movement * MoveSpeed * Time.fixedDeltaTime);
    }

    void Shooting()
    {
        GameObject b;
        if (Input.GetButtonDown("Fire1"))
        {
            b = Instantiate(bulletPrefab, bulletplaceHolder.transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        }
    }
}
