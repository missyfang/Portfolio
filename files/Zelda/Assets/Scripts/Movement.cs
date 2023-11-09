using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // Makes sure the enemy does not continue with their velocity and clip through a wall.
    private void OnDisable()
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
