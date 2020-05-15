using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpticalBenchPlace : MonoBehaviour
{
    public GameObject convexMirrorStand;
    public GameObject concaveLensStand;
    public GameObject objectNeedle;
    public GameObject imageNeedle;
    public ConvexLensNew convexLens;
    public bool isPlaced = false;
    public Animator lowerDeck, upperDeck;
    public Text t;

    public UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        if(uIManager==null){
            Debug.LogError("OpticalBench: UI Manager is missing!");
        }
        Input.simulateMouseWithTouches = true;
    }
    
    void OnMouseDown()
    {
       
        if(uIManager.currentActiveCanvas==null){
            lowerDeck.SetBool("EaseOut",true);
            upperDeck.SetBool("EaseOut",true);
        }
    }

bool islol = true;
    // Update is called once per frame
    void Update()
    {
        // if (Input.touchCount > 0)
        // {
        //     Debug.Log("Touched " );
        //     objectNeedle.SetActive(islol);
        //              islol = !islol;


        //     Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
 
        //      Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
 
        //      //We now raycast with this information. If we have hit something we can process it.
        //      RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
 
        //      if (hitInformation.collider != null) {

        //          //We should have hit something with a 2D Physics collider!
        //          GameObject touchedObject = hitInformation.transform.gameObject;
        //          if(touchedObject.transform.name==gameObject.transform.name){
        //              imageNeedle.SetActive(islol);
        //          }
        //          Debug.Log("Touched " + touchedObject.transform.name);
        //          t.text = touchedObject.transform.name;
        //      }
        //  }

            // Move the cube if the screen has the finger moving.
            
        
        
    }
    public void PlaceImageNeedle(){
        imageNeedle.SetActive(true);
    }
    public void PlaceObjectNeedle(){
        objectNeedle.SetActive(true);
    }
    public void RemoveImageNeedle(){
        imageNeedle.SetActive(false);
    }
    public void RemoveObjectNeedle(){
        objectNeedle.SetActive(false);
    }
    public void placeConcaveLens(){
        if(!isPlaced){
        concaveLensStand.SetActive(true);
        convexLens.isLensPlaced = true;
        convexLens.concaveLens = concaveLensStand.GetComponent<ConcaveLens>();
        convexLens.isPositionChanged = true;


        }
        else{
           concaveLensStand.SetActive(false);
            convexLens.isLensPlaced = false;
            convexLens.concaveLens = null; 
            convexLens.isPositionChanged = true;

        }
        isPlaced = !isPlaced;
    }
    public void placeConvexMirror(){
        if(!isPlaced){
        convexMirrorStand.SetActive(true);
        convexLens.isMirrorPlaced = true;
        convexLens.convexMirror = convexMirrorStand.GetComponent<ConvexMirror>();
        convexLens.isPositionChanged = true;


        }
        else{
           convexMirrorStand.SetActive(false);
            convexLens.isMirrorPlaced = false;
            convexLens.convexMirror = null; 
            convexLens.isPositionChanged = true;

        }
        isPlaced = !isPlaced;

    }
    public void addConvexMirror(){
        convexMirrorStand.SetActive(true);
        convexLens.isMirrorPlaced = true;
        convexLens.convexMirror = convexMirrorStand.GetComponent<ConvexMirror>();
        convexLens.isPositionChanged = true;
        isPlaced = true;

    }
    public void removeConvexMirror(){
        convexMirrorStand.SetActive(false);
            convexLens.isMirrorPlaced = false;
            convexLens.convexMirror = null; 
            convexLens.isPositionChanged = true;
            isPlaced = false;

    }
    public void addConcaveLens(){
        concaveLensStand.SetActive(true);
        convexLens.isLensPlaced = true;
        convexLens.concaveLens = concaveLensStand.GetComponent<ConcaveLens>();
        convexLens.isPositionChanged = true;
        isPlaced = true;

    }
    public void removeConcaveLens(){
        concaveLensStand.SetActive(false);
            convexLens.isLensPlaced = false;
            convexLens.concaveLens = null; 
            convexLens.isPositionChanged = true;
            isPlaced = false;


    }
}
