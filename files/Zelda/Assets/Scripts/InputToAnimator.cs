using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour
{
    Animator animator;
    bool facingRight = true;

    [SerializeField]
    bool isClone = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Switched for clone
        if (isClone)
            facingRight = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (ArrowKeyMovement.playerControl)
        {
            float horizontal_input = Input.GetAxisRaw("Horizontal");

            if (horizontal_input < 0 && facingRight || horizontal_input > 0 && !facingRight)
                Flip();


            animator.SetFloat("horizontal_input", Mathf.Abs(horizontal_input));
            animator.SetFloat("vertical_input", Input.GetAxisRaw("Vertical"));


            // Stop animation if player is not moving
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
                animator.speed = 0.0f;
            else
                animator.speed = 1.0f;
        }

    }

    // Flip player sprite 180 deg.
    void Flip()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        bool flipped = horizontal_input < 0;

        // Flip player
        if (!isClone)
            transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

        // Flip flone opposite of the player
        else
            transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 0f : 180f, 0f));

        facingRight = !facingRight;

    }
}
