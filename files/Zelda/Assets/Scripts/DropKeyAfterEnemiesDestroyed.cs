using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKeyAfterEnemiesDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject key;
    [SerializeField]
    private GameObject Detector;
    [SerializeField]
    private float DetectDistance;
    // Update is called once per frame
    void Update()
    {
        if (!Detect.DetectGameobjectsOfTag("enemy", Detector.transform, DetectDistance))
        {
            key.SetActive(true);
            AudioManager.PlayAudio("keySfx");
            gameObject.SetActive(false);
        }
    }
}
