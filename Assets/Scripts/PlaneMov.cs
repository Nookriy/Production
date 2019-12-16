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

    float nextSoundTime = 0;

    public AudioSource thruster;
    public AudioClip thrustnoise;
    public AudioClip playerbulletsound;
    public AudioSource playeraudiosource;

    public TrailRenderer alttrail;

    public ParticleSystem planetrail;
    
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    public GameObject groundplane;
    float rayLength;

    public Image crosshair;

    Vector3 worldMouse;
    private bool ismoving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletplaceHolder = GameObject.FindGameObjectWithTag("BPlaceHolder");
        playeraudiosource.clip = playerbulletsound;
        thruster.clip = thrustnoise;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        if (ismoving == true)
        {
            if (Time.time >= nextSoundTime)
            {
                thruster.PlayOneShot(thrustnoise);
                nextSoundTime = Time.time + thrustnoise.length;
            }
        }
        else
        {
            nextSoundTime = 0;
        }
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
        bool withChildren = default;
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * y * force);
            planetrail.Play();
            ismoving = true;
        }
        else
        {
            planetrail.Stop(withChildren, stopBehavior: ParticleSystemStopBehavior.StopEmittingAndClear);
            thruster.Stop();
            alttrail.Clear();
            ismoving = false;
        }
    }

    void Shooting()
    {
        GameObject b;
        if (Input.GetButtonDown("Fire1"))
        {
            playeraudiosource.PlayOneShot(playerbulletsound);
            b = Instantiate(bulletPrefab, bulletplaceHolder.transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        }        
    }
}
