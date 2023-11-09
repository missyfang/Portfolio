using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanStunEnemies : MonoBehaviour
{
    public IEnumerator StunEnemy(float stunTime, Collider enemy)
    {
        enemy.GetComponent<Movement>().enabled = false;
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(stunTime);

        enemy.GetComponent<Movement>().enabled = true;
    }
}
