using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct LineEquation{

    //ax+by+c=0
    Vector3 p1,p2;
   
    public float a;
    public float b;
    public float c;
    
    //Vector3 not required but all the points used are in 3D.
    public void setConstants(Vector3 pp1, Vector3 pp2){
        p1 = pp1;
        p2 = pp2;
        a = p1.y-p2.y;
        b = p2.x-p1.x;
        c = p1.x*p2.y - p2.x-p1.y;
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
}