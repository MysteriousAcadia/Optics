using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LineEquation{

    //ax+by+c=0
    public Vector3 p1,p2;
    Vector2 A,B;
   
    public float a;
    public float b;
    public float c;
    
    //Vector3 not required but all the points used are in 3D.
    public void setConstants(Vector3 pp1, Vector3 pp2){
        p1 = pp1;
        p2 = pp2;
        A.x=pp1.x;
        B.x=pp2.x;
        A.y=pp1.y;
        B.y=pp2.y;
        a = p1.y-p2.y;
        b = p2.x-p1.x;
        c = p1.x*p2.y - p2.x-p1.y;
    }

    public Vector3 getConstants(){
        return new Vector3(a,b,c);
    }
    //Returns distance between a point and line.
    public float perpDistance(Vector3 x) {
        Vector3 s = p1-p2;
        Vector3 m1 = x-p2;
        Vector3 cr = Vector3.Cross(m1,s);
        float ans = cr.magnitude/s.magnitude;
        return ans;
    }
    public List<Vector3> getPoints(){
        List<Vector3> points = new List<Vector3>();
        points.Add(p1);
        points.Add(p2);
        return points;
    }

    public float angleBetween(LineEquation l1){
        float tanAngle = (a*l1.b-b*l1.a)/(b*l1.b+a*l1.a);
        float angle = Mathf.Atan2(tanAngle,1);
        return angle*180f/Mathf.PI;
    }

    public Vector3 pointOfIntersection(LineEquation l1){
        Vector3 ans = new Vector3();
        ans.z = p1.z;
        ans.x = (c*l1.b-b*l1.c)/(b*l1.a-a*l1.b);
        return ans;
    }
    public float getSlope(){
        return(-a/b);
    }

     public Vector2 GetClosestPointOnLineSegment(Vector2 P)
    {
        Vector2 AP = P - A;       //Vector from A to P   
        Vector2 AB = B - A;       //Vector from A to B  

        float magnitudeAB = AB.x*AB.x+AB.y*AB.y;     //Magnitude of AB vector (it's length squared)     
        float ABAPproduct = Vector2.Dot(AP, AB);    //The DOT product of a_to_p and a_to_b     
        float distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point  

        if (distance < 0)     //Check if P projection is over vectorAB     
        {
            return A;

        }
        else if (distance > 1)             {
            return B;
        }
        else
        {
            return A + AB * distance;
        }
    }
}