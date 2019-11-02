using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int EnemyHP; 

    public void UpdateEnemyHealth(int enemyHealthChange)
    {
        EnemyHP += enemyHealthChange;
        if (EnemyHP <= 0)
        {
            //Add effects or whatev
            Destroy(gameObject);
        }
    }

}
