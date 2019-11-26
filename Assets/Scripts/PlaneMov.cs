﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    public GameObject groundplane;
    float rayLength;

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
        Controls();
        Shooting();
    }

    void Controls()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        var mouse = Input.mousePosition;
        mouse.z = 0;

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
            rb.AddForce(transform.forward * y * force);
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
