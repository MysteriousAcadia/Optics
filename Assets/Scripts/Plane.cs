using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    XRSurfaceController xRSurface;
    public void ActivateObject(){
        xRSurface = GetComponent<XRSurfaceController>();
        xRSurface.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
