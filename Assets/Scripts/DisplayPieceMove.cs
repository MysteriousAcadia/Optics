using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPieceMove : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] GameObject upperDeck;
    [SerializeField] GameObject lowerDeck;
    [SerializeField] GameObject upperMoveDeck;
    // Start is called before the first frame update
    public Transform origin;
    Vector3 objRelativeToCamera;
    float zobjScreenPos;
    bool isObjectDraggable = false;
    void Start()
    {
        if (upperDeck == null)
        {
            Debug.LogError("Upper Deck not initialised");
        }
        if (lowerDeck == null)
        {
            Debug.LogError("Lower Deck not initialised");
        }
        if (uIManager == null)
        {
            Debug.LogError("UI manager not initialised");
        }
        isObjectDraggable = false;

    }
    void OnMouseDrag()
    {
        if (isObjectDraggable)
        {
            calcRelativePos();
            moveDropper();
        }

    }
    void OnMouseDown()
    {
        if (uIManager.optionSelected < 1)
        {
            uIManager.UpdateDecks(lowerDeck, upperDeck, -1);
        }
        else if (uIManager.optionSelected == 1)
        {
            return;
        }
    }
    private void OnMouseUp()
    {
        isObjectDraggable = false;
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

    public void MoveObjectFunction()
    {
        uIManager.UpdateDecks(upperMoveDeck, 2);
        isObjectDraggable = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(isObjectDraggable){
        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.collider != null)
                {
                    GameObject touchedObject = hit.transform.gameObject;

                    Debug.Log("Touched " + touchedObject.transform.name);
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("This hit at " + hit.point);
                if (hit.collider != null)
                {
                    GameObject touchedObject = hit.transform.gameObject;
                    if (touchedObject.transform.name == "Plane")
                    {
                        calcRelativePos();
                        // moveDropper();
                        transform.position = hit.point;
                        isObjectDraggable = false;
                        uIManager.GoBack();
                    }
                    Debug.Log("Touched " + touchedObject.transform.name);
                }

            }

        }
        }
    }
}
