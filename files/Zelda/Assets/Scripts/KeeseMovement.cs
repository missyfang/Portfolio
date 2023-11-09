using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeseMovement : Movement
{
    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float maxMovePeriod;
    private float movePeriod;
    private float moveCounter;

    [SerializeField]
    LayerMask StopsMovement;

    public float speed;
    Animator animator;

    private float xDirection;
    private float yDirection;

    private Rigidbody rb;

    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        movePeriod = Random.Range(0, maxMovePeriod);
        moveCounter = movePeriod;
    }

    // Update is called once per frame
    void Update()
    {

        // Check if object is in camera view
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject))
        {
            rb.velocity = Vector3.zero;
            animator.speed = 0;
            movePeriod = Random.Range(0, maxMovePeriod);
            moveCounter = movePeriod;
            return;
        }

        if (moveCounter >= movePeriod)
        {
            // Set new move period 
            moveCounter = 0;
            movePeriod = Random.Range(0, maxMovePeriod);

            // Set direction for move period
            xDirection = Random.Range(-1, 1);
            yDirection = Random.Range(-1, 1);

            // Set speed for move period
            if (xDirection == 0 && yDirection == 0)
                speed = 0;
            else
                speed = GetRandomSpeed();

            // Adjust animation playback speed
            animator.speed = speed;
            return;
        }

       
        // Move in opposite direction if run into wall
        if (Physics.Raycast(transform.position, new Vector3(xDirection, yDirection, 0), 0.5f, StopsMovement))
            {
                xDirection = -1 * xDirection;
                yDirection = -1 * yDirection;
            }

        moveCounter += Time.deltaTime;
        rb.velocity = new Vector3(xDirection, yDirection,0) * speed;

    }


    private float GetRandomSpeed()
    {
        float rand = Random.value;
        if (rand <= .9f)
            return Random.Range(1, maxSpeed);

        return 0;
    }
}
