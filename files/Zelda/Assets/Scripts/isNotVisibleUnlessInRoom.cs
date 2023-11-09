using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isNotVisibleUnlessInRoom : MonoBehaviour
{
    private bool isVisible = false;

    void Start()
    {
        int i = 0;
        while (gameObject.transform.GetChild(i) != null)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
            i++;
        }
    }
    void Update()
    {
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject) && isVisible)
        {
            Debug.Log("Make Invisible");
            isVisible = false;
            int i = 0;
            while (gameObject.transform.GetChild(i) != null)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
                i++;
            }

        }
        else if (IsInCameraView.Instance.IsTargetVisible(gameObject) && !isVisible)
        {
            Debug.Log("Make Visible");
            isVisible = true;
            int i = 0;
            while (gameObject.transform.GetChild(i) != null)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
                i++;
            }

        }
    }
}
