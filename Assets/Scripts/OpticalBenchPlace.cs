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
    public GameObject upperDeckGameO,lowerDeckGameO;
    public GameObject upperDeckMountGameO;
    Animator upperDeck, lowerDeck, upperDeckMount;
    public Text t;

    public UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        if(uIManager==null){
            Debug.LogError("OpticalBench: UI Manager is missing!");
        }
        if(upperDeckGameO==null){
            Debug.LogError("OpticalBench: UpperDeckGameO is missing!");
        }
        upperDeck = upperDeckGameO.GetComponent<Animator>();
        lowerDeck = lowerDeckGameO.GetComponent<Animator>();
        Input.simulateMouseWithTouches = true;
    }
    
    void OnMouseDown()
    {
        if(uIManager.optionSelected<0){
       
        if(uIManager.currentLowerDeck==null){
            lowerDeckGameO.SetActive(true);
            lowerDeck.SetBool("EaseOut",true);
        }
        else{
            uIManager.currentLowerDeck.SetActive(false);
            lowerDeckGameO.SetActive(true);
            lowerDeck.SetBool("EaseOut",true);
        }
        if(uIManager.currentUpperDeck==null){
            upperDeckGameO.SetActive(true);
            upperDeck.SetBool("EaseOut",true);
        }
        else{
            uIManager.currentUpperDeck.SetActive(false);
            upperDeckGameO.SetActive(true);
            upperDeck.SetBool("EaseOut",true);
        }
        uIManager.currentLowerDeck = lowerDeckGameO;
        uIManager.currentUpperDeck = upperDeckGameO;
        }
    }

    public void Mount(){
        uIManager.optionSelected = 1;
        uIManager.currentLowerDeck.SetActive(false);
        uIManager.currentUpperDeck.SetActive(false);
        uIManager.previousLowerDeck = uIManager.currentLowerDeck;
        uIManager.currentUpperDeck = upperDeckMountGameO;
        upperDeckMountGameO.SetActive(true);
    }

bool islol = true;
    // Update is called once per frame
    void Update()
    {
        
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
