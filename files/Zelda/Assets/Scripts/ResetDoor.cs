using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject Solver;
    // Update is called once per frame
    void Update()
    {
        if (IsInCameraView.Instance.IsTargetVisible(gameObject)
            && Solver.tag == "solver"
            && !gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            AudioManager.PlayAudio("doorSfx");
        }
    }
}
