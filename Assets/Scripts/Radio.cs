using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Radio : MonoBehaviour
{
    public AudioClip[] songs;

    public AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        Audio.clip = songs[Random.Range(0, 6)];
        Audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
