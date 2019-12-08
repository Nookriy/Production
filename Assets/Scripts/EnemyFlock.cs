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
    //Enemy types
    private int type;

    public bool isFlocking;




    private void Start()
    {
        neighbourEnemies = new GameObject[0]; //Gave memory to the array 
        GetEnemies();
        UpdateAllEnemyArrays();

    }

    public void GetEnemies()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    //Makes all enemies Update their Enemies array
    private void UpdateAllEnemyArrays()
    {
        foreach (GameObject item in Enemies)
        {
            // considering the 2nd child has enemyflock attached to it to get a reference of the freshly spawned Enemy
            item.transform.GetChild(1).GetComponent<EnemyFlock>().GetEnemies();
        }
    }

    private void FixedUpdate()
    {
        GetNeighbourGroup();
        AverageCentre();
        //Checking array; if neighbouring enemies more than 0, then isFlocking. If equal to 0, no flocking.
        if(neighbourEnemies.Length > 0)
        {
            isFlocking = true;
        }
        else if(neighbourEnemies.Length == 0)
        {
            isFlocking = false;
        }
    }


   //This function states that the array has this amount of space based on the number of neighboruing enemies, therefore can only add to that much only

    private void GetNeighbourGroup()
    {
        int i = 0;
        Collider[] col = Physics.OverlapSphere(transform.position, radius);
        //Getting the length of the array (how many enemies are neighboured)
        foreach (var item in col)
        {
            if (item.tag == "Enemy" && item.gameObject != parent)
            {
                i++;
            }
        }
        neighbourEnemies = new GameObject[i];
        //Reseting i, then adding neighboruing enemies to array
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

    //Gets called right after an enemy is destroyed; updates the enemy array 
    private void OnDestroy()
    {
        UpdateAllEnemyArrays();
    }
    //Getting center point of the flock by getting the average position of neighbouring enemies plus itself.
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

    //just to visualize the vcentre to aid in development
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(vcentre, 3f);
    }
}
