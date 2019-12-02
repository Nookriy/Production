using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlock : MonoBehaviour
{
    [SerializeField]
    private GameObject[] neighbourEnemies;
    [SerializeField]
    private GameObject[] Enemies;
    [SerializeField]
    private float radius = 7f;
    public GameObject parent;

    [SerializeField]
    float rotationSpeed;
    public Vector3 vcentre;
    public Vector3 vavoid;

    private int type;

    public bool isFlocking;




    private void Start()
    {
        neighbourEnemies = new GameObject[0];
        GetEnemies();
        UpdateAllEnemyArrays();

    }

    private void UpdateAllEnemyArrays()
    {
        foreach (GameObject item in Enemies)
        {
            // considering the 2nd child has enemyflock attached to it.
            item.transform.GetChild(1).GetComponent<EnemyFlock>().GetEnemies();
        }
    }

    private void FixedUpdate()
    {
        GetNeighbourGroup();
        AverageCentre();
        if(neighbourEnemies.Length > 0)
        {
            isFlocking = true;
        }
        else if(neighbourEnemies.Length == 0)
        {
            isFlocking = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag =="Enemy")
    //    {
    //        GetNeighbourGroup();
    //    }
    //}

    private void GetNeighbourGroup()
    {
        int i = 0;
        neighbourEnemies = new GameObject[0];
        Collider[] col = Physics.OverlapSphere(transform.position, radius);
        foreach (var item in col)
        {
            if (item.tag == "Enemy" && item.gameObject != parent)
            {
                i++;
            }
        }
        neighbourEnemies = new GameObject[i];
        i = 0;
        foreach (var item in col)
        {
            if (item.tag == "Enemy" && item.gameObject != parent)
            {
                neighbourEnemies[i] = item.gameObject;
                i++;
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        GetNeighbourGroup();
    //    }
    //}

    private void OnDestroy()
    {
        UpdateAllEnemyArrays();
    }

    public void GetEnemies()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void FlockingMovement()
    {
        //keeping distance
        //same direction which means to get an average point in the group and make them move towards it/ based on it
    }

    public void AverageCentre()
    {
        float x = 0, y = 0, z = 0;
        foreach (var item in neighbourEnemies)
        {
            x += item.transform.position.x;
            y += item.transform.position.y;
            z += item.transform.position.z;
        }
        x += parent.transform.position.x;
        y += parent.transform.position.y;
        z += parent.transform.position.z;

        vcentre = new Vector3((x / (neighbourEnemies.Length + 1)), (y / (neighbourEnemies.Length + 1)), (z / (neighbourEnemies.Length + 1)));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(vcentre, 3f);
    }
}
