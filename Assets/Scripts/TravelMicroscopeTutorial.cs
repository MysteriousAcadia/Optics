using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelMicroscopeTutorial : MonoBehaviour
{
    public GameObject start,next,previous;
    public Text startText;
    public int step = 0;
    bool isStepChanged = false;
    public TravellingMicroscope travellingMicroscope;
    float finalHorizontalSlider = 1f;
    float finalVerticalSlider = 1f;
    public Slider horizontalSlider;
    public Slider verticalSlider; 
    public bool isTutorialStarted = false;
    public AudioSource audioSource;
    public List<AudioClip> tutorialAudio;
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
            // if(step==0){
            //     travellingMicroscope.emptyWorkingPlace();
            //     travellingMicroscope.hideMicroscopeView();
            //     finalHorizontalSlider = 0f;
            //     finalVerticalSlider = 0f;
            //     travellingMicroscope.hideMicroscopeView();

            // }
            if(step==0){
                travellingMicroscope.emptyWorkingPlace();
                travellingMicroscope.hideMicroscopeView();
                travellingMicroscope.PlaceImage();
                finalHorizontalSlider = 0f;
                finalVerticalSlider = 0f;
            }
            if(step==1){
                travellingMicroscope.emptyWorkingPlace();
                travellingMicroscope.displayMicroscopeView();
                travellingMicroscope.PlaceImage();
                finalHorizontalSlider = 0f;
                finalVerticalSlider = 0.246f;
                
            }
            // if(step==3){
            //     travellingMicroscope.emptyWorkingPlace();
            //     travellingMicroscope.hideMicroscopeView();
            //     travellingMicroscope.PlaceImage();
            //     travellingMicroscope.PlaceGlassSlab();
            //     finalHorizontalSlider = 0f;
            //     finalVerticalSlider = 0.246f;

            // }
            if(step==2){
                travellingMicroscope.emptyWorkingPlace();
                travellingMicroscope.displayMicroscopeView();
                travellingMicroscope.PlaceImage();
                travellingMicroscope.PlaceGlassSlab();
                finalHorizontalSlider = 0f;
                finalVerticalSlider = 0.336f;
            }
            // if(step==5){
            //     travellingMicroscope.emptyWorkingPlace();
            //     travellingMicroscope.hideMicroscopeView();
            //     travellingMicroscope.PlaceImage();
            //     travellingMicroscope.PlaceGlassSlab();
            //     travellingMicroscope.PlaceSprinkle();
            //     finalHorizontalSlider = 0f;
            //     finalVerticalSlider = 0.336f;
            // }
            if(step==3){
                travellingMicroscope.emptyWorkingPlace();
                travellingMicroscope.displayMicroscopeView();
                travellingMicroscope.PlaceImage();
                travellingMicroscope.PlaceGlassSlab();
                travellingMicroscope.PlaceSprinkle();
                finalHorizontalSlider = 0f;
                finalVerticalSlider = 0.508f;
            }
            if(step==4){
                travellingMicroscope.emptyWorkingPlace();
                travellingMicroscope.hideMicroscopeView();
                travellingMicroscope.PlaceImage();
                travellingMicroscope.PlaceGlassSlab();
                travellingMicroscope.PlaceSprinkle();
                finalHorizontalSlider = 0f;
                finalVerticalSlider = 0.508f;
            }
            start.SetActive(false);
            previous.SetActive(false);
            next.SetActive(false);
            audioSource.clip = tutorialAudio[step];
            audioSource.Play();
            Invoke("OnAudioFinish",audioSource.clip.length);
            isStepChanged = false;
        }
        float needleDiff =horizontalSlider.value-finalHorizontalSlider;
        float imageDiff =verticalSlider.value-finalVerticalSlider;

        if(needleDiff!=0){
            if(needleDiff>0){
                horizontalSlider.value -=0.002f;
            }
            else{
                horizontalSlider.value +=0.002f;
            }
        }
        if(imageDiff!=0){
            if(imageDiff>0){
                verticalSlider.value -=0.002f;
            }
            else{
                verticalSlider.value +=0.002f;
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
    void OnAudioFinish(){
        audioSource.Stop();
        start.SetActive(true);
        previous.SetActive(true);
        next.SetActive(true);
    }
}

// 0.2464=1.7
// 0.3357=1.80
// 0.5071=2.007
