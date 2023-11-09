using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player movement script.
public class ArrowKeyMovement : MonoBehaviour
{
    public static bool playerControl;
    public static bool isMoving;
    public static Vector3 attackDirection;
    public static Vector3 direction;

    Rigidbody rb;

    private Vector3 orgPos;
    private Vector3 targPos;
    [SerializeField]
    private float timeToMove = 0.1f;

    public LayerMask StopsPlayerMovement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerControl = true;
        isMoving = false;
    }


    // Update is called once per frame
    void Update()
    {
        // Check if player is allowed to move Link (useful for room transitions).
        if (playerControl && !isMoving)
        {
            if (Input.GetKey("right") || Input.GetKey("d"))
            {
                isMoving = true;
                direction = Vector3.right;
                StartCoroutine(MovePlayer(direction));

            }
            else if (Input.GetKey("left") || Input.GetKey("a"))
            {
                isMoving = true;
                direction = Vector3.left;
                StartCoroutine(MovePlayer(direction));

            }
            else if (Input.GetKey("up") || Input.GetKey("w"))
            {
                isMoving = true;
                direction = Vector3.up;
                StartCoroutine(MovePlayer(direction));

            }
            else if (Input.GetKey("down")|| Input.GetKey("s"))
            {
                isMoving = true;
                direction = Vector3.down;
                StartCoroutine(MovePlayer(direction));
            }
            // Prevent weird movement to the side that was occuring...
            else
            {
                rb.velocity = Vector3.zero;
            }

            // Press 'c' to call back the clone link.
            if (Input.GetKey("c"))
            {
                gameObject.GetComponent<RoomTransition>().GroupUpLinks();
            }
        }
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    // Move player with grid-based movement constraints.
    private IEnumerator MovePlayer(Vector3 direction)
    {
        float elapsedTime = 0;
        orgPos = transform.position;
        targPos = orgPos + (direction)/4.0f;
        float RayDistance = 0.0f;
        BoxCollider LinkCollider = GetComponent<BoxCollider>();
        Vector3 RayPos = transform.position + LinkCollider.center;

        // Move only if target position is not a wall.

        if (direction == Vector3.up
            || direction == Vector3.down)
        {
            RayDistance = 0.3f;
        }
        else
        {
            RayDistance = 0.5f;
        }

        if (!Physics.Raycast(RayPos, direction, RayDistance, StopsPlayerMovement ))  
        {
            while (elapsedTime < timeToMove
                && !Physics.Raycast(RayPos, direction, RayDistance, StopsPlayerMovement))
            {
                transform.position = Vector3.Lerp(orgPos, targPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }


        isMoving = false;
    }

}
