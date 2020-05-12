using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RFWaterTutorial : MonoBehaviour
{
    public GameObject start,next,previous;
    public Text startText;
    public int step = 0;
    bool isStepChanged = false;
    public ConvexMirrorWater convexMirrorWater;

    public GameObject convexLens, dropper;
    float finalVerticalSlider = 1f;
    public Slider verticalSlider; 
    public bool isTutorialStarted = false;
     public AudioSource audioSource;
    public List<AudioClip> tutorialAudio;
       // Start is called before the first frame update

       Vector3 lensOriginalPosition = new Vector3(0.03f,-2.58f,1.68f);
       Vector3 lensNewPosition = new Vector3(0.020f,-1.90f, 2.83f);
       Vector3 dropperOriginalPosition = new Vector3(0.07f,-1.41f,-2.1f);
       Vector3 dropperNewPosition = new Vector3(0.0262f, 1.333f, 2.18f);
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
                // convexMirrorWater.SwitchLiquid(1f);
                // finalVerticalSlider = 0.1f;
                // convexLens.transform.localPosition = lensOriginalPosition;
                // dropper.transform.localPosition = dropperOriginalPosition;
                convexMirrorWater.SwitchLiquid(1f);
                finalVerticalSlider = 3f/8f;
                convexLens.transform.localPosition = lensOriginalPosition;
                dropper.transform.localPosition = dropperOriginalPosition;

            }
            if(step==1){
                // convexMirrorWater.SwitchLiquid(1f);
                // finalVerticalSlider = 3f/8f;
                // convexLens.transform.localPosition = lensOriginalPosition;
                // dropper.transform.localPosition = dropperOriginalPosition;
                convexMirrorWater.SwitchLiquid(1f);
                finalVerticalSlider = 3f/8f;
                convexLens.transform.localPosition = lensNewPosition;
                dropper.transform.localPosition = dropperNewPosition;
                dropper.GetComponent<Dropper>().drops();
            }
            if(step==2){
                convexMirrorWater.SwitchLiquid(1.3f);
                finalVerticalSlider = 3.6f/8f;
                convexLens.transform.localPosition = lensOriginalPosition;
                dropper.transform.localPosition = dropperOriginalPosition;
                // convexMirrorWater.SwitchLiquid(1f);
                // finalVerticalSlider = 3f/8f;
                // convexLens.transform.localPosition = lensNewPosition;
                // dropper.transform.localPosition = dropperOriginalPosition;
                
            }
            // if(step==3){
            //     convexMirrorWater.SwitchLiquid(1f);
            //     finalVerticalSlider =3f/8f;
            //     convexLens.transform.localPosition = lensNewPosition;
            //     dropper.transform.localPosition = dropperNewPosition;

            // }
            // if(step==4){
            //     convexMirrorWater.SwitchLiquid(1f);
            //     finalVerticalSlider = 3f/8f;
            //     convexLens.transform.localPosition = lensNewPosition;
            //     dropper.transform.localPosition = dropperNewPosition;
            //     dropper.GetComponent<Dropper>().drops();
            // }
            // if(step==5){
            //     convexMirrorWater.SwitchLiquid(1.3f);
            //     finalVerticalSlider = 3f/8f;
            //     convexLens.transform.localPosition = lensOriginalPosition;
            //     dropper.transform.localPosition = dropperOriginalPosition;
            // }
            // if(step==6){
            //     convexMirrorWater.SwitchLiquid(1.3f);
            //     finalVerticalSlider = 3.6f/8f;
            //     convexLens.transform.localPosition = lensOriginalPosition;
            //     dropper.transform.localPosition = dropperOriginalPosition;
            // }
           
            start.SetActive(false);
            previous.SetActive(false);
            next.SetActive(false);
            audioSource.clip = tutorialAudio[step];
            audioSource.Play();
            Invoke("OnAudioFinish",audioSource.clip.length);
            isStepChanged = false;
        }
        float imageDiff =verticalSlider.value-finalVerticalSlider;

        if(imageDiff!=0){
            if(imageDiff>0){
                verticalSlider.value -=0.001f;
            }
            else{
                verticalSlider.value +=0.001f;
            }
        }
        
    }
    public void nextStep(){
        if(step<2){
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
    void OnAudioFinish(){
        audioSource.Stop();
        start.SetActive(true);
        previous.SetActive(true);
        next.SetActive(true);
    }
}

//above beaker 0.02627117 1.333542 2.189277
//lens away 0.02007225 -1.903542 2.832467