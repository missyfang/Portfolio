using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int numHealth;

    public void AlterHealth(int changeToHealth)
    {

        if (gameObject.tag == "player" && Cheat.godMode)
            return;

        if (gameObject.tag == "player" && PlayerHitEffect.invincible
            || GoldHItEffect.invincible)
            return;

        numHealth += changeToHealth;
        Debug.Log("change" + numHealth);

        if (gameObject.tag == "player" && numHealth > maxHealth)
        {
            numHealth = maxHealth;
        }

        if (numHealth <= 0 && gameObject.tag != "player")
        {
            AudioManager.PlayAudio("DeathSfx");

            // If not a Keese or Gel... drop loot.
            if (gameObject.GetComponent<HasKey>() != null)
            {
                DropsLoot.DropKey(gameObject.transform);
            }
            else if (gameObject.GetComponent<HasBoomerang>() != null)
            {
                DropsLoot.DropBoomerang(gameObject.transform);
            }
            else if (gameObject.GetComponent<KeeseMovement>() == null
                && gameObject.GetComponent<GelMovement>() == null)
            {
                DropsLoot.DropLoot(gameObject.transform);
            }
            Destroy(gameObject);
        }
        else
        {
            AudioManager.PlayAudio("HitSfx");
        }
    }

}
