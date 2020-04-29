using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBox : MonoBehaviour
{
    public GameObject pin;
    // Start is called before the first frame update
    void Start()
    {
        if(!pin){
            Debug.LogError("PinBox:Pin is not attached!");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Call when user touches the box.
    void OnMouseUp(){
        Instantiate(pin);
    }
}
