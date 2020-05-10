using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
    GameObject liquidDrop;
    public GameObject clamp;
    public Transform origin;


    Vector3 objRelativeToCamera;
    float zobjScreenPos;
    private void OnMouseDown()
    {
        calcRelativePos();
    }

    private void OnMouseDrag()
    {
        moveDropper();
    }

    Vector3 getTouchAsWorldPoint()
    {
        Vector3 touchPoint = Input.mousePosition;
        touchPoint.z = zobjScreenPos;
        touchPoint = Camera.main.ScreenToWorldPoint(touchPoint);
        return touchPoint;
    }

    void calcRelativePos()
    {
        zobjScreenPos = Camera.main.WorldToScreenPoint(this.transform.position).z;
        objRelativeToCamera = this.transform.position - getTouchAsWorldPoint();
    }

    void moveDropper()
    {
        Vector3 localPos = transform.localPosition;
        transform.position = (getTouchAsWorldPoint() + objRelativeToCamera);
    }
  
    public void drops()
    {
        GameObject drop;
        drop = Instantiate(liquidDrop,origin);
        drop.transform.SetParent(clamp.transform);
    }
}
