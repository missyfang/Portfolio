using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanKnockbackEnemies : MonoBehaviour
{
    public float knockback_power;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            /* Diable enemy movement, knockback,and flash effect*/
            other.GetComponent<EnemyHitEffect>().DamageEffect(transform.position);
        }

    }
}
