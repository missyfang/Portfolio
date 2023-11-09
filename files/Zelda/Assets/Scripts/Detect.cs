using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect
{
    public static bool DetectGameobjectsOfTag(string tag, Transform detector, float distance)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);

        for (int i = 0; i < taggedObjects.Length; i++)
        {
            if (Vector3.Distance(taggedObjects[i].transform.position, detector.position) < distance)
            {
                return true;
            }
        }

        return false;
    }
}
