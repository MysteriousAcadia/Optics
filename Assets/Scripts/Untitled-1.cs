// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DrawLine : MonoBehaviour
// {
//     //Variables for line object
//     PencilLine pencilLine;
//     protected LineRenderer line;
//     public Vector3 startPos, endPos;
//     float mZCoordinate;
//     public GameObject currentLine,lineObject;
//     public Prism prism;
//     List<Vector3> prismVertices = new List<Vector3>();
//     List<LineEquation> prismLineEquations = new List<LineEquation>();

//     // Start is called before the first frame update
//     void Start()
//     {
        
        
//     }
    
//     public GameObject drawLine(LineEquation l){
        
//         currentLine = Instantiate(lineObject);
//         pencilLine = currentLine.GetComponent<PencilLine>();
//         currentLine.transform.SetParent(gameObject.transform);
//         currentLine.transform.localPosition = new Vector3(100,110,110);
//         currentLine.transform.position = gameObject.transform.position;
//         line = currentLine.GetComponent<LineRenderer>();
//         l.Point1.z=0.1f;
//         l.Point2.z=0.1f;
//         line.SetPosition(0,l.Point1);
//         line.SetPosition(1,l.Point2);
//         return currentLine;
//     }

//     void OnMouseDown()
//     {
//         mZCoordinate = gameObject.transform.position.z;
//         prismLineEquations = new List<LineEquation>();
//         List<Vector3> prismVertices = prism.getVertices();
//         LineEquation l1 = new LineEquation(prismVertices[0],prismVertices[1]);
//         // l1.setConstants(prismVertices[0],prismVertices[1]);
//         LineEquation l2 = new LineEquation(prismVertices[1],prismVertices[2]);
//         // l2.setConstants(prismVertices[1],prismVertices[2]);
//         LineEquation l3 = new LineEquation(prismVertices[0],prismVertices[2]);
//         // l3.setConstants(prismVertices[0],prismVertices[2]);
//         prismLineEquations.Add(l1);
//         prismLineEquations.Add(l2);
//         prismLineEquations.Add(l3);
//         currentLine = Instantiate(lineObject);
//         pencilLine = currentLine.GetComponent<PencilLine>();
//         currentLine.transform.SetParent(gameObject.transform);
//         currentLine.transform.localPosition = new Vector3(0,0,0);
//         currentLine.transform.position = gameObject.transform.position;
//         line = currentLine.GetComponent<LineRenderer>();

//         RaycastHit hit;
//             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//             if (Physics.Raycast(ray, out hit))
//             {
//                 if(hit.transform.gameObject.name==gameObject.transform.gameObject.name){
//                 foreach(LineEquation l in prismLineEquations){
//                     if(l.perpDistance(new Vector3(hit.point.x,hit.point.y,0f))<0.1f){
//                         Debug.LogError("COULD BE SNAPPED");
//                     }
//                 }

//                 pencilLine.vertices = new List<Vector3>();
//                 pencilLine.vertices.Add(new Vector3(hit.point.x,hit.point.y,0.1f));
//                 line.SetPosition(0,new Vector3(hit.point.x,hit.point.y,0.1f));
//                 line.SetPosition(1,hit.point);
               
//                 }
//             }

//     }
//     private Vector3 GetMouseWorldPos()
//     {
//         Vector3 mousePos = Input.mousePosition;
//         mousePos.z = mZCoordinate;
//         return Camera.main.ScreenToWorldPoint(mousePos);
        
//     }
 
//     void OnMouseDrag()
//     {
//         RaycastHit hit;
//             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//             if (Physics.Raycast(ray, out hit))
//             {
//                if(hit.transform.gameObject.name==gameObject.transform.gameObject.name){
                
//                 line.SetPosition(1,new Vector3(hit.point.x,hit.point.y,0.1f));
//                 pencilLine.vertices[1] = hit.point;
//                }

//             }
     
//     }
//      void OnMouseUp() {
//          RaycastHit hit;
//             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//             if (Physics.Raycast(ray, out hit))
//             {
//                 if(hit.transform.gameObject.name==gameObject.transform.gameObject.name){
//                 bool hasSnapped = false;
//                 foreach(LineEquation l in prismLineEquations){
//                     if(l.perpDistance(new Vector3(hit.point.x,hit.point.y,hit.point.z))<1f){
//                         LineEquation originalLine = new LineEquation(hit.point,line.GetPosition(0));
//                         Vector2 point = l.Intersect(originalLine);
//                         LineEquation l1 = new LineEquation(pencilLine.vertices[0],point);
//                         // l1.setConstants(pencilLine.vertices[0],point);
//                         drawLine(l1);
//                         Debug.LogError("Inputs Given: line1:"+l.ToString()+", line2:"+l1.ToString());
//                         Debug.LogError("Prism Line Equation 1:"+prismLineEquations[0].ToString());
//                         Debug.LogError("Prism Line Equation 2:"+prismLineEquations[1].ToString());
//                         Debug.LogError("Prism Line Equation 3:"+prismLineEquations[2].ToString());
//                         line.SetPosition(1,new Vector3(point.x,point.y,hit.point.z+0.1f));
//                         prism.Calculation(l,l1,prismLineEquations);
//                         // prism.Calculation(l1,l,prismLineEquations);

//                         hasSnapped = true;
//                     }
//                 }
//                 if(!hasSnapped){
//                 line.SetPosition(1,new Vector3(hit.point.x,hit.point.y,0f));
//                 line.SetPosition(1,hit.point);
//                 }
//                 }
                
//             }
 
//     }
// }

