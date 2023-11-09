using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoryiaMovement : Movement
{
    [SerializeField]
    LayerMask StopsMovement;

    [SerializeField]
    float movement_speed;

    [SerializeField]
    float movePeriod;
    private float moveCounter;

    [SerializeField]
    float flipPeriod;
    private float flipCounter;

    [SerializeField]
    int rand;

    SpriteRenderer spriteRender;
    private Animator animator;
    [SerializeField]
    Sprite upSprite;
    [SerializeField]
    Sprite downSprite;
    [SerializeField]
    Sprite sideSprite1;
    [SerializeField]
    Sprite sideSprite2;

    private Vector3[] directions;
    private Vector3 targPos;
    private bool flipped = false;
    Rigidbody rb;

    // Direction to throw 
    public Vector3 throwDirection; 

   
    void Start()
    {
        directions = new Vector3[4] { Vector3.right, Vector3.left, Vector3.up, Vector3.down };
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        movePeriod = Random.value;
        moveCounter = movePeriod;
        flipCounter = flipPeriod;
        rand = Random.Range(0, 4);

    }

  
    void Update()
    {
        // Check if game object is in camera view.
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject))
        {
            animator.enabled = false;
            moveCounter = movePeriod;
            flipCounter = flipPeriod;
            rb.velocity = Vector3.zero;
            return;
        }

        moveCounter -= Time.deltaTime;
        flipCounter -= Time.deltaTime;

        // Reset 
        if (moveCounter < 0)
        {
            movePeriod = Random.value;
            moveCounter = movePeriod;
            rand = Random.Range(0, 4);
        }


        // Move in random direction
        if (rand == 0 && !Physics.Raycast(transform.position, Vector3.right, 0.5f, StopsMovement))
        {
            // Set direction for throw
            throwDirection = Vector3.right;

            // Move
            rb.velocity = new Vector3(movement_speed, 0);

            // Change to correct directional sprite
            spriteRender.sprite = sideSprite1;

            // Roate to correct direction
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

            // Enable sideways walking animation
            animator.enabled = true;
        }

        else if (rand == 1 && !Physics.Raycast(transform.position, Vector3.left, 0.5f, StopsMovement))
        {
            throwDirection = Vector3.left;
            rb.velocity = new Vector3(-movement_speed, 0);
            spriteRender.sprite = sideSprite1;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            animator.enabled = true;
        }

        else if (rand == 2 && !Physics.Raycast(transform.position, Vector3.up, 0.5f, StopsMovement))
        {
            throwDirection = Vector3.up;
            rb.velocity = new Vector3(0, movement_speed);
            spriteRender.sprite = upSprite;

            // Disable sideways walking animation
            animator.enabled = false;
        }

        else if (rand == 3 && !Physics.Raycast(transform.position, Vector3.down, 0.5f, StopsMovement))
        {
            throwDirection = Vector3.down;
            rb.velocity = new Vector3(0, -movement_speed);
            spriteRender.sprite = downSprite;
            animator.enabled = false;
        }
        else
            rand = Random.Range(0, 4);

        // Flip sprite when walking up or down
        if (flipCounter < 0)
        {
            flipped = !flipped;
            flipCounter = flipPeriod;

            if (spriteRender.sprite == upSprite || spriteRender.sprite == downSprite)
                transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
        }




    }

}
