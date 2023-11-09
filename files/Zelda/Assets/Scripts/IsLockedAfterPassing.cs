using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLockedAfterPassing : MonoBehaviour
{
    private GameObject[] Player;
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, Player[0].transform.position) < 1f)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);

            PushPlayersPastDoor();

            this.enabled = false;
        }
    }

    private void PushPlayersPastDoor()
    {
        for (int i = 0; i < Player.Length; i++)
        {
            Player[i].gameObject.transform.position += ArrowKeyMovement.direction;
        }
    }
}
