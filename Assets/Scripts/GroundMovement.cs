using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundMovement : MonoBehaviour
{

    [SerializeField] public float MoveSpeed;
    public Rigidbody PlayerRB;

    [SerializeField]
    float RotationSpeed;
    private GameObject bulletplaceHolder;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float bulletSpeed;

    AudioSource tanksource;

    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    public GameObject groundplane;
    float rayLength;

    public Image crosshair;

    Vector3 Movement;
    Vector3 worldMouse;
    private BattleManager bm;

    void Start()
    {
        tanksource = this.GetComponent<AudioSource>();
        PlayerRB = GetComponent<Rigidbody>();
        bulletplaceHolder = GameObject.FindGameObjectWithTag("BPlaceHolder");
        bm = FindObjectOfType<BattleManager>();
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
        PlayerRB.MovePosition(PlayerRB.position + (Movement * MoveSpeed * Time.fixedDeltaTime));
    }

    void Shooting()
    {
        GameObject b;
        if (Input.GetButtonDown("Fire1"))
        {
            b = Instantiate(bulletPrefab, bulletplaceHolder.transform.position, Quaternion.identity);
            tanksource.PlayOneShot(tanksource.clip);
            b.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            bm.UpdateScore(9);
            bm.UpdatePlayerHealth(-10);
        }
    }
}
