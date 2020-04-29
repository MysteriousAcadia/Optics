using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    Vector3 objRelativeToCamera;
    float zobjScreenPos;

    public GameObject needleHolder;

    [SerializeField]
    float minPos, maxPos;    //SPECIFY IN EDITOR TO CLAMP NEEDLE MOVEMNT ON LOCAL X AXIS

    private void OnMouseDown()
    {
        calcRelativePos();
    }

    private void OnMouseDrag()
    {
        moveNeedle();
    }

    private void Update()
    {
        RestrictNeedleMovement();
    }

    void calcRelativePos()     //CALL IN ONTOUCHDOWN EVENT/FUNCTION BEFORE CALLING MOVENEEDLE IN ONTOUCHDRAG 
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

    void moveNeedle()    //CALL IN ONTOUCHDRAG EVENT/FUNCTION
    {
        Vector3 localPos = transform.localPosition;
        transform.position = new Vector3((getTouchAsWorldPoint() + objRelativeToCamera).x, localPos.y, localPos.z);
    }

    void RestrictNeedleMovement()    //CALL IN UPDATE TO CLAMP NEEDLE MOVEMENT
    {
        transform.position = new Vector3(transform.position.x, needleHolder.transform.position.y, needleHolder.transform.position.z);

        if ((transform.position.x - needleHolder.transform.position.x) >= maxPos)
        { transform.position = new Vector3(needleHolder.transform.position.x+maxPos, transform.position.y, transform.position.z); }
        else if ((transform.position.x - needleHolder.transform.position.x) <= minPos)
        { transform.position = new Vector3(needleHolder.transform.position.x + minPos, transform.position.y, transform.position.z); }
    }
}
