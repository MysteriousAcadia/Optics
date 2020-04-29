using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plumb : MonoBehaviour
{
    [SerializeField]
    GameObject hangingPlumb, restingPlumb;

    GameObject presentInScene;

    bool hanging = true;
 
   //hang() instantiates required plumb line model based on boolean hanging value
   //It also complements the hanging boolean
    void hang()
    {
         if (!hanging)
        {
            Destroy(presentInScene);
            presentInScene = Instantiate(hangingPlumb);
            
        }
        else if (hanging)
        {
            Destroy(presentInScene);
            presentInScene = Instantiate(restingPlumb);
        }
       
        hanging = !hanging;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            hang();
    }
}
