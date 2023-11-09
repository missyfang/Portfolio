using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldHItEffect : MonoBehaviour
{
    public static bool invincible;
    private Rigidbody rb;
    public float flashDuration;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public Material flashMaterial;
    private Coroutine DamageRoutine;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        rb = GetComponent<Rigidbody>();
        invincible = false;

    }

    public void DamageEffect(Vector3 attackerPosition)
    {
        // Player is invincible while effect is playing.
        if (DamageRoutine == null && !Cheat.godMode && !invincible)
        {
            StartCoroutine(ExecuteEffect(attackerPosition));
            rb.velocity = Vector3.zero;
        }
    }

    private IEnumerator ExecuteEffect(Vector3 attackerPosition)
    {
        invincible = true;

        ArrowKeyMovement.playerControl = false;
        // Knockback
        float verticalDiff = attackerPosition.y - gameObject.transform.position.y;
        Debug.Log(verticalDiff);

        if (verticalDiff >= 0)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 2, ForceMode.Impulse);
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
        }


        // Flash
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(flashDuration);
        }

        // Signal damage effects are finished.
        DamageRoutine = null;

        //// Enable user input
        ArrowKeyMovement.playerControl = true;

        // invincible = false;
        StartCoroutine(InvesibilityPeriod());

        rb.velocity = Vector3.zero;
    }


    IEnumerator InvesibilityPeriod()
    {
        yield return new WaitForSeconds(2.0f);
        invincible = false;
    }
}
