using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{

    private BattleManager bm;
    // Start is called before the first frame update
    void Start()
    {
        bm = GameObject.FindObjectOfType<BattleManager>();
        Invoke("SelfDestruct", 3f);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //damage or destroy enemy
            collision.gameObject.GetComponent<EnemyHealth>().UpdateEnemyHealth(-3);
            //add score MAYBE
            bm.UpdateScore(3);
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

