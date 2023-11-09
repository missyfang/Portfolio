using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 18.5f)
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
    }
}
