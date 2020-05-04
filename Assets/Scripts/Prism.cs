using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Prism : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject board,//Provide reference of the board, used to ensure prism is on the board.
         pencilLine;//Provide reference to the prefab of pencilLine.
    [SerializeField]
    protected float n = 1.5f; //Refractive Index
    [SerializeField]
    protected float a = 60f; //Angle of Prism 
    bool isSelected = false;//
    public DrawLine drawLine;
    public void setRotation(float angle){ //Call it to change orientation of prism
        transform.Rotate(new Vector3(0, angle, 0));
        Debug.LogError(circleRadius());
    }
    void setPosition(Vector3 position){
        gameObject.transform.position = position;
        isSelected = false;
    }

    public float getRefractiveIndex(){
        return(n);
    }
    public float getAngleOfPrism(){
        return(a);
    }

    public float circleRadius(){
        List<Vector3> vertices = getVertices();
        float a = Vector3.Distance(vertices[0], vertices[1]);
        float b = Vector3.Distance(vertices[1], vertices[2]);
        float c = Vector3.Distance(vertices[0], vertices[2]);
        float r = a*b*c/ (float)Math.Sqrt((a+b-c)*(b+c-a)*(c+a-b)*(a+b+c));
        return(r);

    }
    
    void OnMouseUp() //Scrip to Move prisim, call it when user touches on the prism
    {
        if(isSelected){
            
        }
        else{
            isSelected = true;
        }
    }
    public List<Vector3> getVertices(){
        Vector3[] meshVerts = gameObject.GetComponent<MeshFilter>().mesh.vertices;
        List<Vector3> vertices = new List<Vector3>();
        foreach(Vector3 meshVert in meshVerts){
            Vector3 meshVertx = transform.TransformPoint(meshVert);
            Debug.LogError(meshVertx);

            meshVertx.z = board.transform.position.z;
            vertices.Add(meshVertx);
            Debug.LogError(meshVertx);

         }
         return(vertices);

    }
    void Start()
    {
        if(!board){
            board = GameObject.Find("Board");
        }
        if(pencilLine && board){
            Debug.LogError("Prism::One of the GameObject is not linked!");
        }
    }
    void changePosition(){
        RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.name==board.transform.gameObject.name){ //Checking if the point is on the board.
                    setPosition(hit.point);
                }
                
            }
    }


    // Update is called once per frame
    void Update()
    {
        if(isSelected && Input.GetMouseButtonUp(0)){
            changePosition();

        }
        if(Input.GetKeyUp("space")){
            drawVertices();
        }
        
    }
    void drawVertices(){
        List<Vector3> vertices = getVertices();
        GameObject p1 = Instantiate(pencilLine);
        p1.transform.SetParent(gameObject.transform);
        p1.transform.localPosition = new Vector3(0,0,0);
        p1.transform.position = gameObject.transform.position;
        p1.GetComponent<PencilLine>().drawLine(vertices[0],vertices[1]);
        GameObject p2 = Instantiate(pencilLine);
        p2.transform.SetParent(gameObject.transform);
        p2.transform.localPosition = new Vector3(0,0,0);
        p2.transform.position = gameObject.transform.position;
        p2.GetComponent<PencilLine>().drawLine(vertices[1],vertices[2]);
        GameObject p3 = Instantiate(pencilLine);
        p3.transform.SetParent(gameObject.transform);
        p3.transform.localPosition = new Vector3(0,0,0);
        p3.transform.position = gameObject.transform.position;
        p3.GetComponent<PencilLine>().drawLine(vertices[2],vertices[0]);

    }
    //Function to calculate angle between 2 lines.(wrtNormal is to get the incidentAngle)
   public float calculateAngle(List<Vector3> line1, List<Vector3> line2, bool wrtNormal){
        float a1 =(float)Math.Atan2(line1[0].y-line1[1].y,line1[0].x-line1[1].x);
        float a2 =(float)Math.Atan2(line2[0].y-line2[1].y,line2[0].x-line2[1].x);
        float angle = (a2-a1) * (180/(float)Math.PI);
        while(!(angle>=0 && angle<=360)){
            if(angle<0){
                angle+=360f;
            }
            else{
                angle-=360f;
            }
        }
        if(wrtNormal){
        if(angle>90){
            return(angle-90f);
        }
        else{
            return(90f-angle);
        }
        }
        else{
            return(angle);
        }
    }
    // line1, line2, and line3 are the equations of line and the planes of the prism
    // All intermediate angles are in degrees.
    public void calculateImageLine(LineEquation line1,LineEquation line2){
        float incidentAngle = calculateAngle(line1.getPoints(),line2.getPoints(),true);
        float refractedAngle1 = radToDeg((float)Math.Asin((float)Math.Sin(degToRad(incidentAngle))/getRefractiveIndex()));
        LineEquation refractedRay = rotatedLine(incidentAngle,line1,line1.pointOfIntersection(line2));

        Debug.LogError("EHERHE");
        drawLine.drawLine(refractedRay);
        float refractedAngle2 = getAngleOfPrism()-refractedAngle1;
        float emergentAngle = radToDeg((float)Math.Asin(getRefractiveIndex()*(float)Math.Sin(degToRad(refractedAngle2))));
        float angleOfDeviation = incidentAngle + emergentAngle - getAngleOfPrism();
    }


    public float degToRad(float deg){
        return(deg*(float)Math.PI/180f);
    }
    public float radToDeg(float rad){
        return(rad*180f/(float)Math.PI);
    }

    LineEquation rotatedLine(float angle,LineEquation l,Vector3 point){
        LineEquation l2 = new LineEquation();
        float rad = degToRad(angle);
        float a = l.b*(float)Math.Sin(rad)+l.a*(float)Math.Cos(rad);
        float b = l.b*(float)Math.Cos(rad)-l.a*(float)Math.Sin(rad);
        float c = l.a*point.x+l.b*point.y+l.c-a-b;
        l2.a = a;
        l2.b = b;
        l2.c = c;
        l2.p1 = point;
        Debug.LogError(l.getConstants().ToString());
        Debug.LogError(l2.getConstants().ToString());
        return l2;
    }


}
