using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletPin : MonoBehaviour
{
    [SerializeField]
    public GameObject board;
    bool isSelected = true;

    void setPosition(Vector3 position){
        gameObject.transform.position = position;
        isSelected = false;
    }
    
    void OnMouseUp()
    {
        Debug.LogError("HERRE");
        if(isSelected){
        }
        else{
            isSelected = true;
        }
    }
    private void Update() {
        if(isSelected && Input.GetMouseButtonUp(0)){
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.LogError(hit.transform.gameObject.name);
                Debug.LogError(board.transform.gameObject.name);
                if(hit.transform.gameObject.name==board.transform.gameObject.name){    
                    setPosition(hit.point);
                }
                
            }

        }
    }

    

}