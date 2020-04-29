using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumbScale : MonoBehaviour
{
    Vector3 nPlanePos, fPlanePos;

    [SerializeField]
    GameObject plumb;

    private void OnMouseDown()
    {
        Vector3 temp = worldTouchPoint();
        measure(temp); ;
    }

    void getTouchAsWorldPoint()
    {
        Vector3 touchPoint = Input.mousePosition;
        touchPoint.z = Camera.main.nearClipPlane;
        nPlanePos = Camera.main.ScreenToWorldPoint(touchPoint);
        touchPoint.z = Camera.main.farClipPlane;
        fPlanePos = Camera.main.ScreenToWorldPoint(touchPoint);
    }

    Vector3 worldTouchPoint()
    {
        getTouchAsWorldPoint();
        RaycastHit hit;
        Physics.Raycast(nPlanePos, fPlanePos - nPlanePos, out hit);
        return hit.point;
    }

    float measure(Vector3 tapPoint)
    {
        float measuredDistance;
        measuredDistance = (tapPoint - plumb.transform.position).magnitude;
        Debug.Log(measuredDistance);
        return measuredDistance;
    }
}
