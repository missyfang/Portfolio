using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasCheckpoints : MonoBehaviour
{
    // Player and Clone
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Clone;

    private GameObject CurrentCheckpoint = null;
    private GameObject[] Checkpoints = new GameObject[7];
    private bool CheckpointReset = false;


    void Start()
    {
        int i = 0;
        while (i < gameObject.transform.childCount)
        {
            Checkpoints[i] = gameObject.transform.GetChild(i).gameObject;
            ++i;
        }
    }
    void Update()
    {

        if (Checkpoints == null)
        {
            return;
        }

        // Generate a checkpoint based on camera view.
        for (int i = 0; i < Checkpoints.Length; i++)
        {
            if (Checkpoints[i] == null)
            {
                return;
            }

            if (IsInCameraView.Instance.IsTargetVisible(Checkpoints[i]))
            {
                Checkpoints[i].SetActive(true);
                CurrentCheckpoint = Checkpoints[i];
            }
            else
            {
                Checkpoints[i].SetActive(false);
            }
        }
    }

    public void GoBackToCheckPoint()
    {
        Debug.Log("Going back to CP");

        CheckpointReset = true;
        // Grab player and clone and place them at the current checkpoint.
        Player.GetComponent<Rigidbody>().MovePosition(CurrentCheckpoint.transform.position);
        Debug.Log(Clone == null);
        Clone.GetComponent<Animator>().enabled = false;
        Clone.GetComponent<Rigidbody>().MovePosition(CurrentCheckpoint.transform.position);

        Clone.GetComponent<Animator>().enabled = true;


        // Give health back to player.
        Player.GetComponent<HasHealth>().numHealth = 6;



    }

    // Used by ResetPuzzle script.
    public bool CheckReset()
    {
        return CheckpointReset;
    }

    // Used by ResetPuzzle script.
    public void FinishedReset()
    {
        CheckpointReset = false;
    }

    public GameObject GetCheckpoint()
    {
        return CurrentCheckpoint;
    }
}
