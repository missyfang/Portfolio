using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMasterController : MonoBehaviour
{
    private GameObject player;
    private bool setBack;
    

    private void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "player" && !Cheat.godMode)
            setBack = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
            setBack = false;

    }
  
    void Start()
    {
        player = GameObject.Find("Player");

    }

    
    void Update()
    {
        // Send player and camera back to starting postions
        if (setBack)
        {
            player.transform.position = new Vector3(39.5800018f, 4.9000001f, 0);
            Camera.main.transform.position = new Vector3(39.5200005f, 6.5f, -10);
        }

    }
}
