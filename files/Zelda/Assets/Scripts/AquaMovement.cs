using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaMovement : Movement
{ 
    [SerializeField]
    Sprite[] sprites = new Sprite[4];

    [SerializeField]
    float movePeriod;
    private float moveCounter;

    [SerializeField]
    float spritePeriod;
    private float spriteCounter;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float fireBallSpeed;


    [SerializeField]
    GameObject fireBallPrefab;

   // private GameObject fireBall1; 
    private int spriteIndex;
    private SpriteRenderer spriteRenderer;
    private Rigidbody rb;
    private Vector3 direction;
    private GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteIndex = 0;

        moveCounter = movePeriod;
        spriteCounter = spritePeriod;
        
        rb = GetComponent<Rigidbody>();

        direction = Vector3.left;

    }

    // Update is called once per frame
    void Update()
    {
        // Check if game object is in camera view.
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject))
        {
            return;
        }

        // Move forward and backward
        rb.velocity = direction * moveSpeed;

        moveCounter -= Time.deltaTime;
        spriteCounter -= Time.deltaTime;

        // Switch direction
        if (moveCounter < 0)
        {
            moveCounter = movePeriod;
            direction *= -1;
        }

        StartCoroutine(ShootFireballs());
    }

    IEnumerator ShootFireballs()
    {
        // Change sprite.
        if (spriteCounter < 0)
        {
            spriteCounter = spritePeriod;
            spriteRenderer.sprite = sprites[spriteIndex];
            spriteIndex += 1;

            if (spriteIndex == 2)
            {
                // Create fireball and set angle 
                GameObject fireBall1 = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
                fireBall1.GetComponent<ShootFireBall>().angle = 20f;
                GameObject fireBall2 = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
                fireBall2.GetComponent<ShootFireBall>().angle = 0f;
                GameObject fireBall3 = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
                fireBall3.GetComponent<ShootFireBall>().angle = -20f;
            }

            yield return new WaitForSeconds(fireBallSpeed);

            if (spriteIndex > 3)
                spriteIndex = 0;
        }
    }
}
