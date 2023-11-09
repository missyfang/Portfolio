using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 1.0f;

    private Rigidbody rb;
    private float currentTime = 0f;

    void Update()
    {
        currentTime += Time.deltaTime;
        // Spin
        transform.Rotate(new Vector3(0,0, 360) * Time.deltaTime * spinSpeed);

        if (currentTime >= 0.175f)
        {
            currentTime = 0f;
            AudioManager.PlayAudio("brangSfx");
        }

    }
}
