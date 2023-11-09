using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineUtilities
{
    public static IEnumerator MoveObjectOverTime(Transform target, Vector3 initialPos, Vector3 finalPos, float durationSec)
    {
        float initialTime = Time.time;
        float progress = (Time.time - initialTime) / durationSec;

        while(progress < 1.0f)
        {
            progress = (Time.time - initialTime) / durationSec;
            Vector3 newPos = Vector3.Lerp(initialPos, finalPos, progress);

            target.position = newPos;

            yield return null;
        }

        target.position = finalPos;
    }
}
public class RoomTransition : MonoBehaviour
{

    public GameObject view;
    public static bool isTransitioning = false;

    private GameObject OtherLink;

    // Start is called before the first frame update
    void Start()
    {
        OtherLink = FindOtherLink();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Handling the Other Link */
    GameObject FindOtherLink()
    {
        GameObject[] Links = GameObject.FindGameObjectsWithTag("player");
        GameObject Other = null;

        for (int i = 0; i < Links.Length; ++i)
        {
            if (Links[i] != gameObject)
            {
                Other = Links[i];
            }
        }

        return Other;
    }

    // New function that ensure both links go into a room together.
    public void GroupUpLinks()
    {
        if (OtherLink != null)
        {
            OtherLink.transform.position = gameObject.transform.position;
        }
    }

    void DisableOtherLink()
    {
        OtherLink.GetComponent<SpriteRenderer>().enabled = false;
    }

    void EnableOtherLink()
    {
        OtherLink.GetComponent<SpriteRenderer>().enabled = true;
    }

    /* Room Transition Core */
    IEnumerator RoomTransitionWithDirection(char direction)
    {
        isTransitioning = true;
        ArrowKeyMovement.playerControl = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        // null check for clone
        if(OtherLink != null)
            DisableOtherLink();

        float xMove = 0.0f;
        float yMove = 0.0f;

        if (direction == 'n')
        {
            yMove = 11.0f;
        }
        else if (direction == 'e')
        {
            xMove = 16.0f;
        }
        else if (direction == 's')
        {
            yMove = -11.0f;
        }
        else if (direction == 'w')
        {
            xMove = -16.0f;
        }

        Vector3 initialPos = view.GetComponent<Transform>().position;

        float x = initialPos.x + xMove;
        float y = initialPos.y + yMove;


        Vector3 finalPos = new Vector3(x, y, -10f);

        yield return StartCoroutine(CoroutineUtilities.MoveObjectOverTime
            (view.GetComponent<Transform>(), initialPos, finalPos, 2.5f));

        if (direction == 'n')
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 2f, 0);
        }
        else if (direction == 'e')
        {
            transform.position = new Vector3(transform.position.x + 2f, transform.position.y, 0);
        }
        else if (direction == 's')
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 2f, 0);
        }
        else if (direction == 'w')
        {
            transform.position = new Vector3(transform.position.x - 2f, transform.position.y, 0);
        }

        ArrowKeyMovement.playerControl = true;
        this.GetComponent<SpriteRenderer>().enabled = true;

        // null check for clone
        if (OtherLink != null)
            EnableOtherLink();

        isTransitioning = false;
        GroupUpLinks();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");

        if (other.CompareTag("northDoor") && !isTransitioning)
        {
            Debug.Log("north");
            StartCoroutine(RoomTransitionWithDirection('n'));
        }
        else if (other.CompareTag("eastDoor") && !isTransitioning)
        {
            Debug.Log("east");
            StartCoroutine(RoomTransitionWithDirection('e'));
        }
        else if (other.CompareTag("southDoor") && !isTransitioning)
        {
            Debug.Log("south");
            StartCoroutine(RoomTransitionWithDirection('s'));
        }
        else if (other.CompareTag("westDoor") && !isTransitioning)
        {
            Debug.Log("west");
            StartCoroutine(RoomTransitionWithDirection('w'));

        }
    }
}
