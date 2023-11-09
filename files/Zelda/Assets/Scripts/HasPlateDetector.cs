using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasPlateDetector : MonoBehaviour
{
    // This will be used to hold all the pressure plates in the room.
    private IsPressurePlate[] PressurePlates;
    private bool Solved = false;

    void Start()
    {
        // Pressure plates must be made as children to the puzzle door.
        // There are less restrictive ways to do this, but it would require a lot more storage.
        PressurePlates = GetComponentsInChildren<IsPressurePlate>();
    }
    void Update()
    {
        // Toggle doors open and closed cheat code
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(Solved);
            if (!Solved)
            {
                Solved = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);

            }
            else
            {
                Solved = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        // Close doors for reset
        if (Input.GetKeyDown("r"))
        {
            Solved = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);

        }
        // If the all pressure plates are pressed, then switch to an open door.
        if (IsSolved() && !Solved
            && IsInCameraView.Instance.IsTargetVisible(gameObject))
        {
            Solved = true;
            AudioManager.PlayAudio("puzzleSfx");
            // Child 0 is the puzzle door and Child 1 is the open door.
            // We could potentially make this less restrictive on format.
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private bool IsSolved()
    {
        // Loop through all child pressure plates.
        for (int i = 0; i < PressurePlates.Length; i++)
        {
            // If any are not pressed, then the puzzle is not solved.
            if (!PressurePlates[i].IsPlatePressed())
            {
                return false;
            }
        }

        // If all are pressed, then the puzzle is solved.
        return true;
    }
}
