using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GelMovement : Movement
{
    [SerializeField]
    LayerMask StopsMovement;

    [SerializeField]
    float speed = 0.1f;

    private float randomDirection;
    private bool isMoving = false;

    private Vector3 orgPos;
    private Vector3 targPos;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        randomDirection = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if game object is in camera view
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject))
            return;

        // Get random distance to fly
        var randomDistance = Random.Range(0, 10);

        if (randomDirection == 0 && !isMoving)
            StartCoroutine(Move(Vector3.right));

        else if (randomDirection == 1 && !isMoving)
            StartCoroutine(Move(Vector3.left));

        else if (randomDirection == 2 && !isMoving)
            StartCoroutine(Move(Vector3.up));

        else if (randomDirection == 3 && !isMoving)
            StartCoroutine(Move(Vector3.down));

      // Set new random direction
       randomDirection = Random.Range(0, 4);

    }
    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        
        // Move random number of grid spaces in direction
        int numSquaresToMove = Random.Range(3, 12);
        while (numSquaresToMove > 0 && !Physics.Raycast(transform.position, direction, 1.0f, StopsMovement))
        {
            float elapsedTime = 0;
            orgPos = transform.position;
            targPos = orgPos + direction;

            // Move one grid space
            while (elapsedTime < speed)
            {
                transform.position = Vector3.Lerp(orgPos, targPos, (elapsedTime / speed));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Stop and snap to grid
            transform.position = targPos;
            yield return new WaitForSeconds(0.25f);

            numSquaresToMove -= 1;
           
        }

        // pause when change direction
        yield return new WaitForSeconds(1);
        isMoving = false;
    }

}
