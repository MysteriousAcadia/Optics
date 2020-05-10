using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMirrorCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject waterBlob;
    public ConvexMirrorWater convexMirrorWater;
    void Start()
    {
        waterBlob.SetActive(false);
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.name=="Water drop(Clone)"){
            waterBlob.SetActive(true);
            convexMirrorWater.SwitchLiquid(1.3f);
            Destroy(collisionInfo.gameObject);
        }
        Debug.LogError("Enter");
        GetComponent<Rigidbody>().useGravity = false;
    }
   public void CleanWater(){
        waterBlob.SetActive(false);
        convexMirrorWater.SwitchLiquid(1f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
