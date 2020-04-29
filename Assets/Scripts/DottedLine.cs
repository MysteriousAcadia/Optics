using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class DottedLine : MonoBehaviour
    {
  public List<Vector3> vertices = new List<Vector3>(2);
  protected LineRenderer line;

     void Start()
    {
        vertices.Add(new Vector3(0,0,0));
        vertices.Add(new Vector3(0,0,0));   
        line = GetComponent<LineRenderer>();
    }

}
