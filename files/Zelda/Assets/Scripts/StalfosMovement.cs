using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalfosMovement : Movement
{
    [SerializeField]
    LayerMask StopsMovement;

    [SerializeField]
    float movePeriod;
    private float moveCounter;

    [SerializeField]
    float flipPeriod;
    private float flipCounter;

    [SerializeField]
    int direction;

    Rigidbody rb;
    public float movement_speed;
    private bool flipped = false;
    private Vector3 targPos; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveCounter = movePeriod;
        flipCounter = flipPeriod;
        direction = Random.Range(0, 4);
    }

    void Update()
    {
        // Check if game object is in camera view.
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject))
        {
            moveCounter = movePeriod;
            flipCounter = flipPeriod;
            rb.velocity = Vector3.zero;
            return;
        }

        moveCounter -= Time.deltaTime;
        flipCounter -= Time.deltaTime;

        // Reset movement interval
        if (moveCounter < 0)
        {
            moveCounter = movePeriod;
            direction = Random.Range(0, 4);
        }

        // Move in random direction only if object will not immediately run into a wall.
        if (direction == 0 && !Physics.Raycast(transform.position, Vector3.right, 0.75f, StopsMovement))
            rb.velocity = new Vector2(movement_speed, 0);

        else if (direction == 1 && !Physics.Raycast(transform.position, Vector3.left, 0.75f, StopsMovement))
            rb.velocity = new Vector2(-movement_speed, 0);

        else if (direction == 2 && !Physics.Raycast(transform.position, Vector3.up, 0.75f, StopsMovement))
            rb.velocity = new Vector2(0, movement_speed);

        else if (direction == 3 && !Physics.Raycast(transform.position, Vector3.down, 0.75f, StopsMovement))
            rb.velocity = new Vector2(0, -movement_speed);

        else
            direction = Random.Range(0, 4);



        // Flip sprite.
        if (flipCounter < 0)
        {
            flipped = !flipped;
            flipCounter = flipPeriod;
            transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
        }
      


    }



}
