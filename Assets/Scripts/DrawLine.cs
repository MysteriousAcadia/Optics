using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    //Variables for line object
    PencilLine pencilLine;
    protected LineRenderer line;
    public Vector3 startPos, endPos;
    float mZCoordinate;
    public GameObject currentLine,lineObject;

    // Start is called before the first frame update
    void Start()
    {
        mZCoordinate = gameObject.transform.position.z;
        
    }

    void OnMouseDown()
    {
        currentLine = Instantiate(lineObject);
        pencilLine = currentLine.GetComponent<PencilLine>();
        currentLine.transform.SetParent(gameObject.transform);
        currentLine.transform.localPosition = new Vector3(0,0,0);
        currentLine.transform.position = gameObject.transform.position;
        line = currentLine.GetComponent<LineRenderer>();

        RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                line.SetPosition(0,new Vector3(hit.point.x,hit.point.y,hit.point.z+0.1f));
                line.SetPosition(1,hit.point);
                pencilLine.vertices[0] = hit.point;
                pencilLine.vertices[1] = hit.point;
            }

    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mZCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePos);
        
    }
 
    void OnMouseDrag()
    {
        RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                line.SetPosition(1,new Vector3(hit.point.x,hit.point.y,hit.point.z+0.1f));
                pencilLine.vertices[1] = hit.point;

            }
     
    }
     void OnMouseUp() {
         RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                line.SetPosition(1,new Vector3(hit.point.x,hit.point.y,hit.point.z+0.1f));
                line.SetPosition(1,hit.point);
            }
 
    }
}

