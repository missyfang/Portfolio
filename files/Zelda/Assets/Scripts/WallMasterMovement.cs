using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMasterMovement : Movement
{
    [SerializeField]
    GameObject[] wallMastersArr = new GameObject[3];
   
    [SerializeField]
    float movePeriod;

    private GameObject player;
    private GameObject wallMaster;
    private bool isMoving;
     
    void Start()
    {
        player = GameObject.Find("Player");
        isMoving = false;
       
    }

    // Triggered when player walks next to a wall
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player" && !isMoving)
        {
            // Select a one of they wallMasters to move
            wallMaster = wallMastersArr[Random.Range(0, 3)];
            StartCoroutine(MoveWallMaster());   
        }        
    }

    IEnumerator MoveWallMaster()
    {
        isMoving = true;

        float rand = Random.value;
        Vector3 orgPos = wallMaster.transform.position;

        // Move in
        float elapsedTime = 0;
        while (elapsedTime < movePeriod)
        {    
            wallMaster.transform.position += wallMaster.transform.up * (1 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move sideways
        elapsedTime = 0;
        while (elapsedTime < movePeriod)
        {
            if (rand < 0.5)
                wallMaster.transform.position += wallMaster.transform.right * (1 * Time.deltaTime);
            else
                wallMaster.transform.position += wallMaster.transform.right * (-1 * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move back
        elapsedTime = 0;
        while (elapsedTime < movePeriod)
        {
            wallMaster.transform.position += wallMaster.transform.up * (-1 * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // return to original position
        wallMaster.transform.position = orgPos;

        isMoving = false;

    }

}
