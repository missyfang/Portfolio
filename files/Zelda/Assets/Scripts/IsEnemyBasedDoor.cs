using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnemyBasedDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject Detector;
    [SerializeField]
    private float DetectDistance;
    private bool Unlocked = false;
    // Update is called once per frame
    void Update()
    {
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject) && !Unlocked)
        {
            Unlocked = true;
            // If player is not in the room, then keep door unlocked
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (IsInCameraView.Instance.IsTargetVisible(gameObject)
            && Detect.DetectGameobjectsOfTag("enemy", Detector.transform, DetectDistance)
            && Unlocked)
        {
            Unlocked = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            AudioManager.PlayAudio("doorSfx");
        }

        if (!Detect.DetectGameobjectsOfTag("enemy", Detector.transform, DetectDistance)
            && IsInCameraView.Instance.IsTargetVisible(gameObject)
            && !Unlocked)
        {
            Unlocked = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            AudioManager.PlayAudio("doorSfx");
        }


    }
}
