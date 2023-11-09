using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrapMovement : Movement
{
    RaycastHit hit;
    Rigidbody rb;
    public Vector3 orgPos;
    IEnumerator moveCoroutine;
    Vector3 directionOfPlayer;
    bool isReturning = false; 

    // Add to increase control of blade trap movements. 
    [SerializeField]
    bool moveVertically = true;
    [SerializeField]
    bool moveHorizontally = true;
    [SerializeField]
    float speed;
    [SerializeField]
    float returnSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        orgPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is in path of blade traps
        if (transform.position == orgPos)
        {
            moveCoroutine = MoveTowardsPlayer();
            // right
            if (Physics.Raycast(new Vector3 (transform.position.x, transform.position.y - 0.4f), Vector3.right, out hit) && hit.transform.CompareTag("player") && moveHorizontally)
            {
                directionOfPlayer = Vector3.right;
                StartCoroutine(MoveTowardsPlayer());
            }
            // left
            else if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.4f), Vector3.left, out hit) && hit.transform.CompareTag("player") && moveHorizontally)
            {
                directionOfPlayer = Vector3.left;
                StartCoroutine(MoveTowardsPlayer());
            }
            // up
            else if (Physics.Raycast(transform.position, Vector3.up, out hit) && hit.transform.CompareTag("player") && moveVertically)
            {
                directionOfPlayer = Vector3.up;
                StartCoroutine(MoveTowardsPlayer());
            }
            // down
            else if (Physics.Raycast(transform.position, Vector3.down, out hit) && hit.transform.CompareTag("player") && moveVertically)
            {
                directionOfPlayer = Vector3.down;
                StartCoroutine(MoveTowardsPlayer());
            }
        }

    }

    IEnumerator MoveTowardsPlayer()
    { 
        rb.velocity = directionOfPlayer * speed;
        yield return null; 
    }

    IEnumerator ReturnToOriginalPosition()
    {
        isReturning = true;
        while (Vector3.Distance(transform.position, orgPos) > 0.1f)
        {
            rb.velocity = directionOfPlayer * -1 * returnSpeed; 
            yield return null;
        }

            rb.velocity = Vector3.zero;
            transform.position = orgPos;
        isReturning = false;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bladeTrap")
        {
            // stop movement
            StopCoroutine(moveCoroutine);
            // rebound
            StartCoroutine(ReturnToOriginalPosition());
        }

        if(other.tag == "puzzleBlock" && isReturning)
        {
            // stop movement
            StopAllCoroutines();

            // move to one over from pushable block
            rb.velocity = Vector3.zero;
            Vector3 direction = (transform.position - other.transform.position).normalized;
            Debug.Log(direction);
            orgPos = other.transform.position + direction;
            transform.position = other.transform.position + direction;
            isReturning = false;

        }

        if (other.tag == "wall")
        {
           // rebound
            StartCoroutine(ReturnToOriginalPosition());
        }
    }
}