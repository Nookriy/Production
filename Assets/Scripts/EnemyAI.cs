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

        velocitE = direction * force;

        //enemy.transform.LookAt(player.transform);
        rb.velocity = velocitE;
    }
}
