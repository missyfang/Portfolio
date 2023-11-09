using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    public static bool invincible;
    private Rigidbody rb;
    public float knockbackForce;
    public float flashDuration;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public Material flashMaterial;
    private Coroutine DamageRoutine;
    [SerializeField]
    private LayerMask StopsEnemyKnockback;
    [SerializeField]
    private bool isStunned = true;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        rb = GetComponent<Rigidbody>();
        invincible = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "weapon")
        {
            /* Diable enemy movement, knockback,and flash effect*/
            DamageEffect(other.GetComponent<Weapon>().positionUsed);
        }

    }

    public void DamageEffect(Vector3 attackerPosition)
    {
        if (DamageRoutine == null)
        {
            StartCoroutine(ExecuteEffect(attackerPosition));
            rb.velocity = Vector3.zero;
        }
    }

    private IEnumerator ExecuteEffect(Vector3 attackerPosition)
    {
        invincible = true;
        // KnockBack.
        var direction = CalculateKnockBack((attackerPosition).normalized);
        //rb.velocity = direction * knockbackForce;
        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);

        if (isStunned)
        {
            // Temporarily disable enemy movement.
            gameObject.GetComponent<Movement>().enabled = false;
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

        // Enable enemy movement.
        gameObject.GetComponent<Movement>().enabled = true;

        invincible = false;

        rb.velocity = Vector3.zero;
    }

    private Vector3 CalculateKnockBack(Vector3 direction, float gridSize = 1.0f)
    {
        Debug.Log(direction);
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

        // Prevents enemy from being pushed over walls
        while (Physics.Raycast(transform.position, roundedDirection, knockbackForce, StopsEnemyKnockback))
        {
            Debug.Log("Slowing Down Enemy");
            knockbackForce -= 1f;
        }
        Debug.Log(knockbackForce);
        Debug.Log(roundedDirection);

        return roundedDirection;

    }
}
