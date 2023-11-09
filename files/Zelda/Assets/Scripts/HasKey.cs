using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasKey : MonoBehaviour
{
    void Start()
    {
        GameObject KeyMask = Resources.Load("keyMask") as GameObject;
        Instantiate(KeyMask, gameObject.transform);
    }
}
