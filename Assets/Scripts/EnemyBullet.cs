using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int BulletTimer = 5;
    private BattleManager bm;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", BulletTimer);
        bm = GameObject.FindObjectOfType<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //damage or destroy player
            bm.UpdatePlayerHealth(-1);
            //play audio
            //
            SelfDestruct();
        }
        else
        if (other.gameObject.tag == "Wall")
        {
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
