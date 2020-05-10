using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorLensConvexTutorial : MonoBehaviour
{
    public GameObject start,next,previous;
    public Text startText;
    public int step = 0;
    bool isStepChanged = false;
    public ConvexLensNew convexLensNew;
    public OpticalBenchPlace opticalBenchPlace;
    float finalObjectNeedlePos = 1f;
    float finalImageNeedlePos = 1f;
    public Slider objectNeedleSlider;
    public Slider imageNeedleSlider; 
    public bool isTutorialStarted = false;
       // Start is called before the first frame update
    public void StartTutorial()
    {
        if(!isTutorialStarted){
        isStepChanged = true;
        step = 0;
        startText.text = "Stop Tutorial";
        previous.SetActive(true);
        next.SetActive(true);
        }
        else{
        startText.text = "Start Tutorial";
        previous.SetActive(false);
        next.SetActive(false);
        }
        isTutorialStarted = !isTutorialStarted;
        
    }
    void Start(){
        isTutorialStarted = false;
        startText.text = "Start Tutorial";
        previous.SetActive(false);
        next.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(!isTutorialStarted){
            return;
        }
        if(isStepChanged){
            if(step==0){
                finalObjectNeedlePos = 1f;
                finalImageNeedlePos = 1f;
                opticalBenchPlace.removeConvexMirror();
            }
            if(step==1){
                convexLensNew.imageVisible = true;
                finalObjectNeedlePos = 1f;
                finalImageNeedlePos = 1f;
                opticalBenchPlace.addConvexMirror();
            }
            if(step==2){
                opticalBenchPlace.addConvexMirror();
                convexLensNew.imageVisible = true;
                finalObjectNeedlePos = 3.8f/5f;
                finalImageNeedlePos = 1f;
                Debug.LogError(finalImageNeedlePos);
                
            }
            if(step==3){
                opticalBenchPlace.removeConvexMirror();
                convexLensNew.imageVisible = true;
                finalObjectNeedlePos = 3.8f/5f;
                finalImageNeedlePos = 1f;

            }
            if(step==4){
                opticalBenchPlace.removeConvexMirror();
                convexLensNew.imageVisible = true;
                finalObjectNeedlePos = 3.8f/5f;
                finalImageNeedlePos = -convexLensNew.gameO.transform.localPosition.x/5f;

            }
            isStepChanged = false;
        }
        float needleDiff =objectNeedleSlider.value-finalObjectNeedlePos;
        float imageDiff =imageNeedleSlider.value-finalImageNeedlePos;

        if(needleDiff!=0){
            if(needleDiff>0){
                objectNeedleSlider.value -=0.002f;
            }
            else{
                objectNeedleSlider.value +=0.002f;
            }
        }
        if(imageDiff!=0){
            if(imageDiff>0){
                imageNeedleSlider.value -=0.002f;
            }
            else{
                imageNeedleSlider.value +=0.002f;
            }
        }
        
    }
    public void nextStep(){
        if(step<4){
            step++;
            isStepChanged = true;
        }
        
    }
    public void previousStep(){
        if(step>0){
            step--;
            isStepChanged = true;
        }
    }
}
