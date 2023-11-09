using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int damageToPlayer;
    public int enemyHealth = 2;

    void Update()
    {
        if (this.gameObject != null && enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
