using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = -1;
    public float speed = 8f;
    public bool rotate = true;
    public string type = "sword";
    public Vector3 positionUsed;

    private void OnEnable()
    {
        positionUsed = ArrowKeyMovement.attackDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") && type != "brang")
        {
            other.gameObject.GetComponent<HasHealth>().AlterHealth(damage);
            Destroy(gameObject, 0.1f);
        }

        if (other.gameObject.CompareTag("wall") && type != "brang")
        {

            Destroy(gameObject, 0.1f);
        }

    }


}
