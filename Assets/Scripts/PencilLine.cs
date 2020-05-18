using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PencilLine : MonoBehaviour
{
    //0 start position, 1 end position;
    public List<Vector3> vertices = new List<Vector3>(2);
    public LineRenderer lineRenderer;
    public LineEquation lineEquation;
    MeshCollider meshCollider;


    public void AddColliderToLine()
    {
        lineRenderer = GetComponent<LineRenderer>();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        meshCollider.sharedMesh = mesh;
    }

/// <summary>
/// OnMouseDown is called when the user has pressed the mouse button while
/// over the GUIElement or Collider.
/// </summary>
void OnMouseDown()
{
    Debug.LogError("Line clicked");

}



    void Start()
    {
        vertices.Add(new Vector3(0,0,0));
        vertices.Add(new Vector3(0,0,0)); 
        lineRenderer = GetComponent<LineRenderer>();  
        meshCollider = gameObject.AddComponent<MeshCollider>();
             
    }

 
    public void drawLine(Vector3 a, Vector3 b){
        lineRenderer.SetPosition(0,a);
        lineRenderer.SetPosition(1,b);
        // lineEquation.setConstants(a,b);
    }

 
    
   
}
