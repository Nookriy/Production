using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneMov : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float force;
    [SerializeField]
    float RotationSpeed;
    private GameObject bulletplaceHolder;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float bulletSpeed;

    public ParticleSystem planetrail;
    
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    public GameObject groundplane;
    float rayLength;

    public Image crosshair;

    Vector3 worldMouse;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletplaceHolder = GameObject.FindGameObjectWithTag("BPlaceHolder");
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Controls();
        Shooting();
    }

    void Controls()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
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
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * y * force);
            planetrail.Play();
        }
        else
            planetrail.Stop();
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
