using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShootFireBall : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;
    private Rigidbody rb;

    [SerializeField]
    float movePeriod;
    private float moveCounter;

    [SerializeField]
    float fireBallSpeed;

    public float angle;

    private Vector3 direction;
    private Vector3 targetPos;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = GameObject.Find("Player");
        playerPos = player.transform.position;

        moveCounter = movePeriod;

        direction = Quaternion.Euler(0, 0, angle) * (playerPos - transform.position);
        rb.velocity = direction.normalized * fireBallSpeed;

    }

    public void Update()
    { 
        
        moveCounter -= Time.deltaTime;

        if (moveCounter < 0)
            Destroy(gameObject);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
            Destroy(gameObject);
    }


}
