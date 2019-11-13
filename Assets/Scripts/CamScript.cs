using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    private PlaneMov player;

    public Vector3 offset;

    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlaneMov>();
        mainCam = Camera.main;
        mainCam.transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        mainCam.transform.position = player.transform.position + offset;
    }
}
