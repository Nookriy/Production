using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", 3f);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //damage or destroy enemy
            //add score MAYBE
            //play audio
            //
            SelfDestruct();
        }
    }

    public void SelfDestruct()
        {
            Destroy(gameObject);
        }
    }

