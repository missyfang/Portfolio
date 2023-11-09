using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPressurePlate : MonoBehaviour
{
    // A LayerMask for flexible items that could press the pressure plate.
    [SerializeField]
    LayerMask PressingObject;

    // A bool to signify that this pressure plate is currently pressed.
    private bool IsPressed = false;
    // Update is called once per frame
    void Update()
    {
        // Send a raycast upward to see if a suitable object (block or player) is standing on pressure plate.
        if (Physics.Raycast(transform.position, Vector3.forward, 1f, PressingObject)
            && !IsPressed)
        {
            // If so, it is pressed.
            IsPressed = true;
            AudioManager.PlayAudio("doorSfx");
        }

        if (!Physics.Raycast(transform.position, Vector3.forward, 1f, PressingObject))
        {
            // If not, it is not pressed.
            IsPressed = false;
        }
    }

    // This function is to maintain the security of this class.
    public bool IsPlatePressed()
    {
        return IsPressed;
    }
}
