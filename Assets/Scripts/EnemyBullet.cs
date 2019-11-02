using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private BattleManager bm;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", 3f);
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
            bm.UpdatePlayerHealth(-3);
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
