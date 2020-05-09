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
    public Prism prism;
    List<Vector3> prismVertices = new List<Vector3>();
    List<LineEquation> prismLineEquations = new List<LineEquation>();

    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    
    public void drawLine(LineEquation l){
        
        currentLine = Instantiate(lineObject);
        pencilLine = currentLine.GetComponent<PencilLine>();
        currentLine.transform.SetParent(gameObject.transform);
        currentLine.transform.localPosition = new Vector3(100,110,110);
        currentLine.transform.position = gameObject.transform.position;
        line = currentLine.GetComponent<LineRenderer>();
        l.p1.z=0.1f;
        l.p2.z=0.1f;
        line.SetPosition(0,l.p1);
        line.SetPosition(1,l.p2);
    }

    void OnMouseDown()
    {
        mZCoordinate = gameObject.transform.position.z;
        prismLineEquations = new List<LineEquation>();
        List<Vector3> prismVertices = prism.getVertices();
        LineEquation l1 = new LineEquation();
        l1.setConstants(prismVertices[0],prismVertices[1]);
        LineEquation l2 = new LineEquation();
        l2.setConstants(prismVertices[1],prismVertices[2]);
        LineEquation l3 = new LineEquation();
        l3.setConstants(prismVertices[0],prismVertices[2]);
        prismLineEquations.Add(l1);
        prismLineEquations.Add(l2);
        prismLineEquations.Add(l3);
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
                if(hit.transform.gameObject.name==gameObject.transform.gameObject.name){
                foreach(LineEquation l in prismLineEquations){
                    if(l.perpDistance(new Vector3(hit.point.x,hit.point.y,0f))<0.1f){
                        Debug.LogError("COULD BE SNAPPED");
                    }
                }

                line.SetPosition(0,new Vector3(hit.point.x,hit.point.y,0.1f));
                line.SetPosition(1,hit.point);
               
                }
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
               if(hit.transform.gameObject.name==gameObject.transform.gameObject.name){
                
                line.SetPosition(1,new Vector3(hit.point.x,hit.point.y,0.1f));
                pencilLine.vertices[1] = hit.point;
               }

            }
     
    }
     void OnMouseUp() {
         RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.name==gameObject.transform.gameObject.name){
                bool hasSnapped = false;
                foreach(LineEquation l in prismLineEquations){
                    if(l.perpDistance(new Vector3(hit.point.x,hit.point.y,hit.point.z))<1f){

                        Vector2 point = l.GetClosestPointOnLineSegment(new Vector2(hit.point.x,hit.point.y));
                        LineEquation l1 = new LineEquation();
                        l1.setConstants(pencilLine.vertices[0],point);
                        line.SetPosition(1,new Vector3(point.x,point.y,hit.point.z+0.1f));
                        prism.calculateImageLine(l,l1,prismLineEquations);
                        hasSnapped = true;
                    }
                }
                if(!hasSnapped){
                line.SetPosition(1,new Vector3(hit.point.x,hit.point.y,0f));
                line.SetPosition(1,hit.point);
                }
                }
                
            }
 
    }
}

