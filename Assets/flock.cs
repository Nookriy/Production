using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float rotationSpeed;
    Vector3 averageHeading;
    Vector3 averagePosition;
    
    public float neighbourDistance = 7f;
    public float avoidanceDist = 2f;

    bool turning = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= globalFlock.tankSize)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }
            

        if (turning)
        {
                Vector3 direction = Vector3.zero - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                speed = Random.Range(0.5f, 1);
        }
        else
        {
                if (Random.Range(0, 5) < 1)
                    ApplyRules();
            
        }
       
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = globalFlock.allCubes;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float groupSpeed = 0.1f;

        Vector3 goalPos = globalFlock.goalPos;
         
        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position,transform.position);
                if (dist <= neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (dist < avoidanceDist)
                    {
                        vavoid += (this.transform.position - go.transform.position);
                    }

                    flock anotherFlock = go.GetComponent<flock>();
                    groupSpeed = groupSpeed + anotherFlock.speed;
                }
            } 
        }

        if (groupSize > 0)
        {
            // Calculate average centre of group
              vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            // Calculate average speed of group
            speed = groupSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }
}
