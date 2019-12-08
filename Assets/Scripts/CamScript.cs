using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    private PlaneMov player;

    public float minDistx,maxDistx,minDisty, maxDisty, camSpeed;
    

    public Vector3 offset;

    private Camera mainCam;

    Rigidbody camrb;
    Rigidbody playerrb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlaneMov>();
        mainCam = Camera.main;
        mainCam.transform.position = player.transform.position + offset;
        camrb = mainCam.GetComponent<Rigidbody>();
        playerrb = player.GetComponent<Rigidbody>();
        //Debug.Log(Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position));
        //Debug.Log((transform.position.x - player.transform.position.x) + " , " + (transform.position.y - player.transform.position.y));
    }
    
    // Update is called once per frame
    void Update()
    {
        CamFollow();
        
    }

    private void CamFollow()
    {
        //y dist between -9 and 9 camera will not follow OR x dist between 20 and -20 camm wil not follow
        //mainCam.transform.position = player.transform.position + offset;

        //Vector3 followSpot = player.transform.position + offset;
        
        //Creating a virtual rectangle based on the current position of the player and the camera; if player gets out of the rectangle, camera's velocity is set to the player's velocity.
        //Else cam velocity is 0.
        if (transform.position.x - player.transform.position.x > maxDistx || transform.position.x - player.transform.position.x < minDistx ||
           transform.position.z - player.transform.position.z > maxDisty || transform.position.z - player.transform.position.z < minDisty)
        {
            //mainCam.transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset, camSpeed);
            camrb.velocity = new Vector3(playerrb.velocity.x, 0, playerrb.velocity.z);
        }
        else
        {
            camrb.velocity = Vector3.zero;
        }
        //Debug.Log(Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position));
        //Debug.Log((transform.position.x - player.transform.position.x) + " , " + (transform.position.y - player.transform.position.y));
    }
}

