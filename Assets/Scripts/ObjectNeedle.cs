using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNeedle : MonoBehaviour
{

    public float needleValueAtScale;

    GameObject convexLens;
    GameObject dragController;
    // Start is called before the first frame update
    void Start()
    {
        convexLens = FindObjectOfType<ConvexLensNew>().gameObject;
        //dragController = FindObjectOfType<DragController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
