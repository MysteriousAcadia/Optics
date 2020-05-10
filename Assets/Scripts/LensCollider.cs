using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensCollider : MonoBehaviour
{
    Vector3 objRelativeToCamera;
    float zobjScreenPos;
    public ConvexMirrorWater convexMirrorWater;
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.name=="Plane Mirror"){
        convexMirrorWater.SetActive(true);
        convexMirrorWater.isPositionChanged = true;

        }
    }
    void OnCollisionStay(Collision collisionInfo)
    {
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.name=="Plane Mirror"){
            convexMirrorWater.SetActive(false);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        calcRelativePos();
    }

    private void OnMouseDrag()
    {
        moveLens();
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

    void moveLens()
    {
        Vector3 localPos = transform.localPosition;
        transform.position = (getTouchAsWorldPoint() + objRelativeToCamera);
    }
  
}
