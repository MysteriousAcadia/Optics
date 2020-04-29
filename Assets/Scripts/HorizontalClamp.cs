using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalClamp : MonoBehaviour
{
    Vector3 objRelativeToCamera;
    float zobjScreenPos;

    [SerializeField]
    float minPos, maxPos;

    private void OnMouseDown()
    {
        CalcRelativePos();
    }

    private void OnMouseDrag()
    {
        MoveClamp();
    }

    private void Update()
    {
        RestrictMovement(minPos, maxPos);
    }

    void CalcRelativePos()   //CALL IN ONTOUCH EVENT/FUNCTION BEFORE CALLING MOVECLAMP IN ONTOUCHDRAG EVENT/FUNCTION
    {
        zobjScreenPos = Camera.main.WorldToScreenPoint(this.transform.position).z;
        objRelativeToCamera = this.transform.position - getTouchAsWorldPoint();
    }

    Vector3 getTouchAsWorldPoint()
    {
        Vector3 touchPoint = Input.mousePosition;
        touchPoint.z = zobjScreenPos;
        touchPoint = Camera.main.ScreenToWorldPoint(touchPoint);
        return touchPoint;
    }

    void MoveClamp()     //CALL IN ONTOUCHDRAG EVENT/FUNCTION
    {
        Vector3 localPos = transform.localPosition;
        transform.position = new Vector3(localPos.x, (getTouchAsWorldPoint() + objRelativeToCamera).y, localPos.z);
    }

    void RestrictMovement(float min, float max)
    {
        if (transform.position.y >= max)
        { transform.position = new Vector3(transform.position.x, max,  transform.position.z); }
        else if (transform.position.y <= min)
        { transform.position = new Vector3(transform.position.x, min, transform.position.z); }
    }
}
