using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    private PlaneMov player;

    public float minDistx,maxDistx,minDisty, maxDisty, camSpeed;
    

    public Vector3 offset;

    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlaneMov>();
        mainCam = Camera.main;
        mainCam.transform.position = player.transform.position + offset;
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
        if (transform.position.x - player.transform.position.x > maxDistx || transform.position.x - player.transform.position.x < minDistx ||
           transform.position.y - player.transform.position.y > maxDisty || transform.position.y - player.transform.position.y < minDisty)
        {
            mainCam.transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset, camSpeed);
        }
        //Debug.Log(Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position));
        //Debug.Log((transform.position.x - player.transform.position.x) + " , " + (transform.position.y - player.transform.position.y));
    }
}

