using System.Collections;
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

        if (Input.GetKey(KeyCode.W)) 
            rb.AddForce(transform.up * y * force);

        transform.Rotate(new Vector3(0,0,RotationSpeed * -x), Space.Self);

        if(Input.GetKeyDown(KeyCode.S))
        {
            transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, -180);
            rb.MoveRotation(Quaternion.Euler(this.rb.rotation.x, this.rb.rotation.y, -180));
        }
    }

    void Shooting()
    {
        GameObject b;
        if (Input.GetButtonDown("Fire1"))
        {
            b = Instantiate(bulletPrefab, bulletplaceHolder.transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().AddForce(transform.up * bulletSpeed);
        }        
    }
}
