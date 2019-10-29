using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Rigidbody rb;

    Vector3 direction;

    Vector3 velocitE;

    [SerializeField]
    float force;

    public GameObject bulletplaceHolder;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float bulletSpeed;
    float timer = 0;
    [SerializeField]
    int waitingTime;

    bool fire = false;

    public GameObject player;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = enemy.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - enemy.transform.position).normalized;

        enemy.transform.LookAt(player.transform);

        velocitE = direction * force;

        //enemy.transform.LookAt(player.transform);
        rb.velocity = velocitE;

        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            //Action
            timer = 0;
            Shooting();
        }
    }

    void Shooting()
    {
        GameObject b;
        Debug.Log("shot");
        b = Instantiate(bulletPrefab, bulletplaceHolder.transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed,ForceMode.Impulse);
    }
}

