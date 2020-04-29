using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDottedLine : MonoBehaviour
{
    public List<Vector3> vertices = new List<Vector3>();
     public bool scaleInUpdate = false;
     private LineRenderer lR;
     private Renderer rend;
     private void Start ()
     {
         ScaleMaterial();
         enabled = scaleInUpdate;
     }
     public void ScaleMaterial()
     {
         lR = GetComponent<LineRenderer>();
         rend = GetComponent<Renderer>();
         rend.material.mainTextureScale =
             new Vector2(Vector2.Distance(lR.GetPosition(0), lR.GetPosition(lR.positionCount - 1))*0.1f / (lR.widthMultiplier),
                 1);
     }
     private void Update ()
     {
        var line = gameObject.GetComponent<LineRenderer>();
        var distance = Vector3.Distance(lR.GetPosition(0), lR.GetPosition(lR.positionCount - 1));
        line.materials[0].mainTextureScale = new Vector3(distance*3, 1, 1);
        //  rend.material.mainTextureScale =
        //      new Vector2(Vector2.Distance(lR.GetPosition(0), lR.GetPosition(lR.positionCount - 1)) / lR.widthMultiplier,
        //          1);
     }
      float perpDistance(Vector3 p1, Vector3 p2, Vector3 x) {
    Vector3 d = (x - vertices[1]) / Vector3.Distance(x,vertices[1]);
    Vector3 v = vertices[0] - vertices[1];
    float t = Vector3.Dot(v,d);
    Vector3 P = vertices[1] +  new Vector3(d.x*t,d.y*t,d.z*t);
    return Vector3.Distance(P,vertices[0]);
}
}
