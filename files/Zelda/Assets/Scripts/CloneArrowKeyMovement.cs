using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherit from arrowKeyMovement so clone can still push blocks without adding code to CanPushBlocks.
public class CloneArrowKeyMovement : MonoBehaviour
{
  
   /* Vars not needed by clone at this time */
    //public static Vector3 attackDirection;
    public static Vector3 direction;
    public LayerMask StopsPlayerMovement;


    // Had to rename variables bc of inheritence
    public static bool cloneIsMoving;

    Rigidbody cloneRb;
    
    private Vector3 cloneOrgPos;
    private Vector3 cloneTargPos;
    private float cloneTimeToMove = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        cloneRb = GetComponent<Rigidbody>();
        cloneIsMoving = false;
    }


    // Update is called once per frame
    void Update()
    {
        // Clone can only move if Link can.
        if (ArrowKeyMovement.playerControl && !cloneIsMoving)
        {
            /* clone right and left are switch so horizontal movement mirrors links */
            if (Input.GetKey("left") || Input.GetKey("a"))
            {
                cloneIsMoving = true;
                direction = Vector3.right;
                StartCoroutine(MovePlayer(direction));

            }
            else if (Input.GetKey("right") || Input.GetKey("d"))
            {
                cloneIsMoving = true;
                direction = Vector3.left;
                StartCoroutine(MovePlayer(direction));

            }
            else if (Input.GetKey("up") || Input.GetKey("w"))
            {
                cloneIsMoving = true;
                direction = Vector3.up;
                StartCoroutine(MovePlayer(direction));

            }
            else if (Input.GetKey("down") || Input.GetKey("s"))
            {
                cloneIsMoving = true;
                direction = Vector3.down;
                StartCoroutine(MovePlayer(direction));
            }
            // Prevent weird movement to the side that was occuring...
            else
            {
                cloneRb.velocity = Vector3.zero;
            }
        }
    }

    /* Func not needed by clone at this time */
    /*
    public Vector3 GetDirection()
    {
        return direction;
    }*/


    // Move player with grid-based movement constraints.
    private IEnumerator MovePlayer(Vector3 direction)
    {
        float elapsedTime = 0;
        cloneOrgPos = transform.position;
        cloneTargPos = cloneOrgPos + (direction) / 4.0f;
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

        if (!Physics.Raycast(RayPos, direction, RayDistance, StopsPlayerMovement))
        {
            while (elapsedTime < 0.04f
                && !Physics.Raycast(RayPos, direction, RayDistance, StopsPlayerMovement))
            {
                transform.position = Vector3.Lerp(cloneOrgPos, cloneTargPos, (elapsedTime / 0.04f));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }


        cloneIsMoving = false;
    }

}
