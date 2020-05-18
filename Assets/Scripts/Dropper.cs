using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
    GameObject liquidDrop;
    public GameObject clamp;
    public Transform origin;
    public UIManager uIManager;
    public GameObject upperDeckDropper,lowerDeckDropper;
    bool isObjectMovable;

    Vector3 objRelativeToCamera;
    float zobjScreenPos;
    private void OnMouseDown()
    {
        if (uIManager.optionSelected < 1)
        {
            uIManager.UpdateMenu(lowerDeckDropper, upperDeckDropper, 4);
        }
        else if (uIManager.optionSelected == 1)
        {
            return;
        }
    }
    
     void Update()
    {
        if(isObjectMovable)
        {
            if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                calcRelativePos();
                moveDropper();
                uIManager.GoBack();
                isObjectMovable = false;

            }
            if (Input.GetMouseButton(0))
            {
                float planeY = this.transform.position.y;
                Transform draggingObject = transform;


                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit distance; // the distance from the ray origin to the ray intersection of the plane
                if (Physics.Raycast(ray, out distance))
                {
                    draggingObject.position = ray.GetPoint(distance.distance);
                    float xPos =draggingObject.position.x;
                    float zPos =draggingObject.position.z;
                    draggingObject.position = new Vector3(xPos, transform.position.y, zPos);
                    uIManager.GoBack();
                    Debug.LogError("DROOPPER BACK");
                    isObjectMovable = false;// distance along the ray
                }
                

            }
        }
               
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
    public void moveObjectPosition(){
        uIManager.UpdateDecks(upperDeckDropper,2);
        isObjectMovable = true;
    }
}
