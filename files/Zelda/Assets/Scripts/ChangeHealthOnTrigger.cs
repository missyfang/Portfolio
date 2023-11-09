using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeHealthOnTrigger : MonoBehaviour
{
    public int health_delta;
    public float knockback_power;
    private void OnTriggerStay(Collider other)
    {
        /* Check if is clone and call correct alter health and damage effect*/
        if (other.gameObject.GetComponent<CloneHasHealth>() != null)
        {
            other.gameObject.GetComponent<CloneHasHealth>().AlterPlayerHealth(health_delta);
            other.gameObject.GetComponent<GoldHItEffect>().DamageEffect(gameObject.transform.position);
            return;
        }

        /* Alter health of other */
        HasHealth other_health = other.gameObject.GetComponent<HasHealth>();
        if (other_health == null || other.tag != "player" || gameObject.GetComponent<Movement>() != null && !gameObject.GetComponent<Movement>().enabled)
            return;
        if (Cheat.godMode && other.tag == "player")
            return;
      
        
        /* Alter player health*/
        other.GetComponent<HasHealth>().AlterHealth(health_delta);
        
        /*Gold scene damage effect*/
        if (other.gameObject.GetComponent<GoldHItEffect>() != null)
            other.gameObject.GetComponent<GoldHItEffect>().DamageEffect(gameObject.transform.position);

        /* Regular scene damage effect. Disable player controls, knockback,and flash effect*/
        else
            other.GetComponent<PlayerHitEffect>().DamageEffect(transform.position);
        

      

    }
}
