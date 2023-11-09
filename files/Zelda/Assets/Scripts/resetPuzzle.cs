using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPuzzle : MonoBehaviour
{
    private Vector3 InitialPos;
    private string InitialTag;
    private HasCheckpoints CheckpointSystem = null;
    void Start()
    {
        CheckpointSystem = FindObjectOfType<HasCheckpoints>();
        InitialPos = gameObject.transform.position;
        InitialTag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject))
        {
            Reset();
        }

        if (Input.GetKey("r")
            || (CheckpointSystem != null
            && CheckpointSystem.CheckReset()))
        {
            Reset();
            CheckpointSystem.FinishedReset();
        }

    }


    // Reset Blocks and blade traps 
    public void Reset()
    {
        if (InitialTag == "bladeTrap")
            gameObject.GetComponent<BladeTrapMovement>().orgPos = InitialPos;

        gameObject.tag = InitialTag;
        gameObject.transform.position = InitialPos;
    }
}
