using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exp1Tutorial : MonoBehaviour
{
    public GameObject start,next,previous;
    public Text startText;
    public int step = 0;
    bool isStepChanged = false;
    public ConvexLensNew convexLensNew;
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
        audioSource = GetComponent<AudioSource>();
        if(audioSource==null){
            Debug.LogError("No Audio Source Found");
        }
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
            start.SetActive(false);
            previous.SetActive(false);
            next.SetActive(false);
            if(step==0){
                finalObjectNeedlePos = convexLensNew.focalLength*2f/5f;
                finalImageNeedlePos = 1f;
                audioSource.clip = tutorialAudio[step];
                audioSource.Play();
                Invoke("OnAudioFinish",audioSource.clip.length);
            }
            if(step==1){
                finalObjectNeedlePos = convexLensNew.focalLength*2f/5f;
                finalImageNeedlePos = (convexLensNew.focalLength*2f/5f);
                audioSource.clip = tutorialAudio[step];
                audioSource.Play();
                Invoke("OnAudioFinish",audioSource.clip.length);
            }
            if(step==2){
                finalObjectNeedlePos = convexLensNew.focalLength*2f/5f;
                finalImageNeedlePos = (convexLensNew.focalLength*2f/5f);
                audioSource.clip = tutorialAudio[step];
                audioSource.Play();
                Invoke("OnAudioFinish",audioSource.clip.length);
            }
            if(step==3){
                finalObjectNeedlePos = convexLensNew.focalLength*2f/5f;
                finalImageNeedlePos = (convexLensNew.focalLength*2f/5f);
                audioSource.clip = tutorialAudio[step];
                audioSource.Play();
                Invoke("OnAudioFinish",audioSource.clip.length);
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
        if(step<3){
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
