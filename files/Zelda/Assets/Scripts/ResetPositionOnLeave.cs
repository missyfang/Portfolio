using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionOnLeave : MonoBehaviour
{
    private Transform originalPos;
    private bool isInOriginalPos = true;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject) && !isInOriginalPos)
        {
            isInOriginalPos = true;
            gameObject.transform.position = originalPos.position;
        }

        if (gameObject.transform.position != originalPos.position)
        {
            isInOriginalPos = false;
        }
    }
}
