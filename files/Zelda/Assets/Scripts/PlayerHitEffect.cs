using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    public static bool invincible; 
    private Rigidbody rb;
    public float knockbackForce;
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
        Vector3 orgPos = transform.position;

        invincible = true;
        // KnockBack.
        var direction = CalculateKnockBack((transform.position - attackerPosition).normalized);
        //rb.velocity = direction * knockbackForce;
        rb.AddForce(direction * knockbackForce, ForceMode.VelocityChange);

        // Temporarily disable user input.
        ArrowKeyMovement.playerControl = false;

        // Flash
        StartCoroutine(Flash());

        float loopDuration = 0.0f;
        // Allows for faster Knockback speeds by checking if Link has traveled two cells and then stopping his movement.
        while (Vector3.Distance(orgPos, gameObject.transform.position) < 1f
            && loopDuration < 1f)
        {
            loopDuration += Time.deltaTime;
            if (rb.velocity == Vector3.zero)
            {
                break;
            }
        }

        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(0.2f);

        // Signal damage effects are finished.
        DamageRoutine = null;

        // Enable user input
        ArrowKeyMovement.playerControl = true;

        yield return new WaitForSeconds(1f);

        invincible = false;
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(flashDuration);
        }
    }

    private Vector3 CalculateKnockBack(Vector3 direction, float gridSize = 1.0f)
    {
        Debug.Log("calc knockback");
        // Snap to grid
        Vector3 roundedDirection = new Vector3(
            Mathf.Round(direction.x / gridSize) * gridSize,
            Mathf.Round(direction.y / gridSize) * gridSize,
            0);

        // Grid movement
        if (Mathf.Abs(roundedDirection.x) > Mathf.Abs(roundedDirection.y))
        {
            roundedDirection.y = 0;
        }
        else
        {
            roundedDirection.x = 0;
        }

        // A sad attempt at preventing player from being pushed over walls
        while (Physics.Raycast(transform.position, roundedDirection, knockbackForce, 
            gameObject.GetComponent<ArrowKeyMovement>().StopsPlayerMovement))
        {
            knockbackForce -= 0.5f;
        }


        return roundedDirection;

    }
}
