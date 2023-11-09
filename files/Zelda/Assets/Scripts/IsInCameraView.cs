using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IsInCameraView : MonoBehaviour
{
    public static IsInCameraView Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool IsTargetVisible(GameObject go)
    {
        if (RoomTransition.isTransitioning)
            return false;

        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }
}
