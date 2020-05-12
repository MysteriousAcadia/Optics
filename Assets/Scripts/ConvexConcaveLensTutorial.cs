using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvexConcaveLensTutorial : MonoBehaviour
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
            if(step==0){
                // finalObjectNeedlePos = 1f;
                // finalImageNeedlePos = 1f;
                // opticalBenchPlace.removeConcaveLens();
                 convexLensNew.imageVisible = true;
                finalObjectNeedlePos = 1f;
                finalImageNeedlePos = 1f;
                opticalBenchPlace.removeConcaveLens();
            
            }
            if(step==1){
                convexLensNew.imageVisible = true;
                finalObjectNeedlePos = 1f;
                finalImageNeedlePos = -convexLensNew.gameO.transform.localPosition.x/5f;
                Debug.LogError(finalImageNeedlePos);
                opticalBenchPlace.removeConcaveLens();
                // convexLensNew.imageVisible = true;
                // finalObjectNeedlePos = 1f;
                // finalImageNeedlePos = 1f;
                // opticalBenchPlace.removeConcaveLens();
            }
            if(step==2){
                convexLensNew.imageVisible = true;
                finalObjectNeedlePos = 1f;
                finalImageNeedlePos = -convexLensNew.gameO.transform.localPosition.x/5f;
                Debug.LogError(finalImageNeedlePos);
                opticalBenchPlace.removeConcaveLens();
            }
            if(step==3){
                convexLensNew.imageVisible = true;
                finalObjectNeedlePos = 1f;
                finalImageNeedlePos = -convexLensNew.gameO.transform.localPosition.x/5f;
                opticalBenchPlace.addConcaveLens();

            }
            if(step==4){
                convexLensNew.imageVisible = true;
                opticalBenchPlace.addConcaveLens();
                finalObjectNeedlePos = 1f;
                finalImageNeedlePos = -convexLensNew.gameO.transform.localPosition.x/5f;

            }
            if(step==5){
                 convexLensNew.imageVisible = true;
                opticalBenchPlace.addConcaveLens();
                finalObjectNeedlePos = 1f;
                finalImageNeedlePos = -convexLensNew.gameO.transform.localPosition.x/5f;
            }
            if(step==6){
                 convexLensNew.imageVisible = true;
                opticalBenchPlace.addConcaveLens();
                finalObjectNeedlePos = 1f;
                finalImageNeedlePos = -convexLensNew.gameO.transform.localPosition.x/5f;
            }
             start.SetActive(false);
            previous.SetActive(false);
            next.SetActive(false);
            audioSource.clip = tutorialAudio[step];
            audioSource.Play();
            Invoke("OnAudioFinish",audioSource.clip.length);
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
        if(step<6){
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
