using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour {

  // Adjust this if the transform isn't at the bottom edge of the object
  public float heightAdjustment = 0.0f;

  // Prefab to instantiate.  If null, the script will instantiate a Cube
  public GameObject prefab;
  public GameObject gameButton;
  // Scale factor for instantiated GameObject
  public float objectScale = 1.0f;
  public bool isObjectPlaced = false;

  private GameObject myObj;

  void Update() {
    // Tap to place
    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !isObjectPlaced ) {

      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
      // The "Surface" GameObject with an XRSurfaceController attached should be on layer "Surface"
      // If tap hits surface, place object on surface
      if(Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Surface"))) {
        CreateObject(new Vector3(hit.point.x, hit.point.y + heightAdjustment, hit.point.z));
        isObjectPlaced = true;
        gameButton.SetActive(false);
      } 
    }
  }

  void CreateObject(Vector3 v) {
    // If prefab is specified, Instantiate() it, otherwise, place a Cube
    if (prefab) {
      myObj = GameObject.Instantiate(prefab);
    } else {
      Debug.LogError("Add prefab");
    }
    myObj.transform.position = v;
    myObj.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
  }
}