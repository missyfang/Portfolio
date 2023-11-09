using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPushBlocks : MonoBehaviour
{
    [SerializeField]
    private LayerMask StopsPushing;
    private bool isPushing = false;
    private bool isTouching = false;
    private GameObject[] PuzzleDoors;

    void Start()
    {
        PuzzleDoors = GameObject.FindGameObjectsWithTag("puzzleDoor");
    }

    void Update()
    {
       
        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position, gameObject.GetComponent<ArrowKeyMovement>().GetDirection(), out hit, 0.5f);

        if (hit.collider != null 
            && (hit.collider.gameObject.tag == "pushable" 
            || hit.collider.gameObject.tag == "solver"
            || hit.collider.gameObject.tag == "puzzleBlock"))
        {
            isTouching = true;
          
            if (!isPushing)
            {
                Debug.Log("Detected Pushable Object");
                isPushing = true;
                StartCoroutine(CheckPush(hit.collider.gameObject));
            }
        }
        else
        {
            isTouching = false;
        }
    }

    IEnumerator CheckPush(GameObject ObjectToPush)
    {
        string pushDirection;
        string pushDirection2;

        if (Input.GetKey("right")|| Input.GetKey("d"))
        {
            pushDirection = "right";
            pushDirection2 = "d";
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            pushDirection = "left";
            pushDirection2 = "q";
        }
        else if (Input.GetKey("up") || Input.GetKey("w"))
        {
            pushDirection = "up";
            pushDirection2 = "w";
        }
        else 
        {
            pushDirection = "down";
            pushDirection2 = "s";
        }
        yield return new WaitForSeconds(0.5f);

        if ((Input.GetKey(pushDirection) || Input.GetKey(pushDirection2)) && isTouching
            && !Physics.Raycast(ObjectToPush.transform.position, gameObject.GetComponent<ArrowKeyMovement>().GetDirection(), 1.0f, StopsPushing))
        {
            Debug.Log("Push Succeeded");

            if (ObjectToPush.tag == "solver")
            {
                AudioManager.PlayAudio("PuzzleSfx");
                GameObject SolvedDoor = null;

                for (int i = 0; i < PuzzleDoors.Length; ++i)
                {
                    if (IsInCameraView.Instance.IsTargetVisible(PuzzleDoors[i]))
                    {
                        SolvedDoor = PuzzleDoors[i];
                    }
                }

                Solve(SolvedDoor);
            }

            if (ObjectToPush.tag != "puzzleBlock")
            {
                ObjectToPush.tag = "Untagged";
            }
            StartCoroutine(PushObject(ObjectToPush, gameObject.GetComponent<ArrowKeyMovement>().GetDirection()));
        }
        else
        {
            Debug.Log("Push Failed");
            isPushing = false;
        }
    }

    IEnumerator PushObject(GameObject ObjectToPush, Vector3 direction)
    {
        Debug.Log("Attempting to Push");
        float timeToMove = 1f;
        float elapsedTime = 0;
        Vector3 orgPos = ObjectToPush.transform.position;
        Vector3 targPos = orgPos + direction;
        Debug.Log("Direction" + direction.ToString());

        while (elapsedTime < timeToMove)
        {
            ObjectToPush.transform.position = Vector3.Lerp(orgPos, targPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure object is in correct position. 
        ObjectToPush.transform.position = targPos;
        isPushing = false;
    }

    void Solve(GameObject door)
    {
        if (door != null)
        {
            AudioManager.PlayAudio("doorSfx");

            door.transform.GetChild(0).gameObject.SetActive(false);
            door.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
