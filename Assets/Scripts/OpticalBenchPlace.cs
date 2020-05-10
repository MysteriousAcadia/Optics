using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpticalBenchPlace : MonoBehaviour
{
    public GameObject convexMirrorStand;
    public GameObject concaveLensStand;
    public ConvexLensNew convexLens;
    public bool isPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        convexMirrorStand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void placeImageScreen(){

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
