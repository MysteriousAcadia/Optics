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
        gameObject.transform.position = new Vector3(position.x,position.y,gameObject.transform.position.z);
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

            meshVertx.z = 0.2f;
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
        if(angle>180){
            angle = 360f-angle;
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
    public void calculateImageLine(LineEquation line1,LineEquation line2,List<LineEquation> prismLineEquations){
        // float incidentAngle = calculateAngle(line1.getPoints(),line2.getPoints(),true);
        Debug.LogError(line1.getConstants().ToString()+" "+line2.getConstants().ToString());
        float incidentAngle = 90f-line1.angleBetween(line2);
        float refractedAngle1 = radToDeg((float)Math.Asin((float)Math.Sin(degToRad(incidentAngle))/getRefractiveIndex()));
        Debug.LogError("RefractedAngle"+refractedAngle1);
        LineEquation refractedRay = rotatedLine(-90+refractedAngle1,line1,line2.p2);//works
        // LineEquation refractedRay = rotatedLine(refractedAngle1-incidentAngle,line2,line2.pointOfIntersection(line1));
        LineEquation refractedRaytry = rotatedLine(-refractedAngle1+incidentAngle,line2,line2.pointOfIntersection(line1));
        LineEquation normal1 = rotatedLine(90f,line1,line1.pointOfIntersection(line2));
        if(refractedRaytry.angleBetween(normal1)<refractedRay.angleBetween(normal1)){
            refractedRay = refractedRaytry;
        }

        
        int minPos = 0;
        float minDist = 10000f;
        int i = 0;
        foreach(LineEquation l in prismLineEquations){
            float dist = line1.perpDistance(refractedRay.pointOfIntersection(l));
            Debug.LogError(dist);
                    if(minDist>dist&&dist>0.01f){
                        minPos = i;
                        minDist = dist;
                    }
                    i++;
                }
        Debug.LogError(minDist);
        refractedRay.p2 = refractedRay.pointOfIntersection(prismLineEquations[minPos]);
        refractedRay.p2.z = refractedRay.p1.z;
        drawLine.drawLine(refractedRay);
        // drawLine.drawLine(refractedRay1);
        float refractedAngle2 =90f-refractedRay.angleBetween(prismLineEquations[minPos]);
        float emergentAngle;
        if(Math.Sin(degToRad(refractedAngle2))*getRefractiveIndex()<1){
        emergentAngle = radToDeg((float)Math.Asin((float)Math.Sin(degToRad(refractedAngle2))*getRefractiveIndex()));
        }
        else{
            Debug.LogError("TIRRR");
            emergentAngle = refractedAngle2;
        }
        Debug.LogError("RF:"+refractedAngle2);
        Debug.LogError("RF:"+Math.Sin(degToRad(refractedAngle2)));
        Debug.LogError("RF:"+degToRad(refractedAngle2)*getRefractiveIndex());
        Debug.LogError(emergentAngle);
        LineEquation refractedRay2 = rotatedLine(refractedAngle2-emergentAngle,refractedRay,refractedRay.pointOfIntersection(prismLineEquations[minPos]));
        LineEquation refractedRay3 = rotatedLine(-refractedAngle2+emergentAngle,refractedRay,refractedRay.pointOfIntersection(prismLineEquations[minPos]));
        // drawLine.drawLine(refractedRay2);
        if(refractedRay2.angleBetween(prismLineEquations[minPos])>refractedRay3.angleBetween(prismLineEquations[minPos])){
        drawLine.drawLine(refractedRay3);
        }
        else{
            drawLine.drawLine(refractedRay2);
        }
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
        l2.p2 = new Vector3();
        l2.p2.x = point.x-20;
        l2.p2.y = ((-a*point.x*(-20))-c)/b;
        l2.p2.z = 0.2f;
        l2.p1.z = 0.2f;
        return l2;
    }




    


}
