using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    private bool isTouching = false;
    private bool isUnlocking = false;

    void Update()
    {

        RaycastHit hit;
        Physics.Raycast(gameObject.transform.position, gameObject.GetComponent<ArrowKeyMovement>().GetDirection(), out hit, 1.0f);
       

        if (hit.collider != null && hit.collider.gameObject.tag == "lockedDoor"
            && inventory.GetKeys() > 0)
        {
            isTouching = true;
           
            if (!isUnlocking)
            {
                Debug.Log("Detected Unlockable Object");
                isUnlocking = true;
                StartCoroutine(CheckUnlock(hit.collider.gameObject));
            }
        }
        else
        {
            isTouching = false;
        }
    }

    IEnumerator CheckUnlock(GameObject DoorToUnlock)
    {
        // For up/down left/right keys
        string pushDirection;
        // For WASD keys
        string pushDirection2;

        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            pushDirection = "right";
            pushDirection2 = "d";
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            pushDirection = "left";
            pushDirection2 = "a";

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
        yield return new WaitForSeconds(0.2f);

        Debug.Log("Checking Push");
        Debug.Log(pushDirection);
        Debug.Log(isTouching);

        if (Input.GetKey(pushDirection) && isTouching || Input.GetKey(pushDirection2) && isTouching)
        {
            Debug.Log("Push Succeeded");
            DoorToUnlock.tag = "Untagged";
            Unlock(DoorToUnlock);
        }
        else
        {
            Debug.Log("Push Failed");
            isUnlocking = false;
        }
    }

    void Unlock(GameObject door)
    {
        AudioManager.PlayAudio("doorSfx");

        door.transform.GetChild(0).gameObject.SetActive(false);
        door.transform.GetChild(1).gameObject.SetActive(true);
        door.GetComponent<Collider>().enabled = false;

        inventory.SubKeys(1);
        isUnlocking = false;
    }
}
