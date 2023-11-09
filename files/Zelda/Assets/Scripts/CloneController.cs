using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    [SerializeField]
    private GameObject PuzzleControls;
    void Start()
    {
        // Clone not visiable at start.
        PuzzleControls.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PuzzleControls.SetActive(true);
    }
}

  