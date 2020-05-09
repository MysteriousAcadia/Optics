using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TravellingMicroscope : MonoBehaviour
{
    public float focalLength = 1.2f;
    List<GameObject> onStage = new List<GameObject>();
    List<Image> onStageView = new List<Image>();
    GameObject underObservation;
    public Slider horizontal,vertical;
    public Text textHorizontal, textVertical;
    public GameObject horizontalSlider, verticalSlider, plane, glassSlab, sprinkles;
    public GameObject microscopeView;
    public Image crossImage,sprinklesImage,blurImage;
    public bool isMicroscopeViewVisible = false;

    public void MoveSliderVertically(){
        float slide = vertical.value;
        float maxPos = -180f;
        float minPos = -80f;
        float finalDist = minPos+slide*(maxPos-minPos);
        verticalSlider.transform.localPosition = new Vector3(verticalSlider.transform.localPosition.x,finalDist,verticalSlider.transform.localPosition.z);
        textVertical.text = verticalSlider.transform.position.y.ToString();
        CalculateImage();

    }
    public void MoveSliderHorizontally(){
        float slide = horizontal.value;
        float maxPos = -4f;
        float minPos = -0f;
        float finalDist = minPos+slide*(maxPos-minPos);
        horizontalSlider.transform.localPosition = new Vector3(horizontalSlider.transform.localPosition.x,finalDist,horizontalSlider.transform.localPosition.z);

    }
    public void ToggleView(){
        isMicroscopeViewVisible = !isMicroscopeViewVisible;
        microscopeView.SetActive(isMicroscopeViewVisible);
    }
    
    // Start is called before the first frame update
    void Start()
    {
      isMicroscopeViewVisible = false;
      microscopeView.SetActive(false);  
    }


    void CalculateImage(){
        bool isGlassSlabPresent = false;
        float thickness = 0f;
        float index = 0f;
        for(int i = onStage.Count-1;i>=0;i--){
            GameObject gameObject = onStage[i];
            if(gameObject.name=="GlassSlab"){
                Debug.LogError("PREENT"+i);
                isGlassSlabPresent = true;
                thickness = gameObject.GetComponent<GlassSlab>().thickness;
                index = gameObject.GetComponent<GlassSlab>().refractiveIndex;
                continue;

            }
            if(!isGlassSlabPresent){
                Vector3 yPos1 = verticalSlider.transform.position;
                Vector3 yPoss2 = gameObject.transform.position;
                Debug.LogError("SPRINL:E");
                Material mat = Instantiate(blurImage.material);
                float blurAmt = focalLength+thickness*(index-1)+(yPoss2.y-yPos1.y);
                if(blurAmt<0.001f && blurAmt>-0.001f){
                mat.SetFloat("_Size",0);
                }
                else{
                mat.SetFloat("_Size",blurAmt*8);                    
                }                blurImage.material = mat;
                Debug.LogError(focalLength+thickness*(index-1)+(yPoss2.y-yPos1.y));
                break;
                
            }
            else{
                Vector3 yPos1 = verticalSlider.transform.position;
                Vector3 yPoss2 = gameObject.transform.position;
                Material mat = Instantiate(blurImage.material);
                float blurAmt = focalLength+thickness*(index-1)+(yPoss2.y-yPos1.y);
                if(blurAmt<0.001f && blurAmt>-0.001f){
                mat.SetFloat("_Size",0);
                }
                else{
                mat.SetFloat("_Size",blurAmt*8);                    
                }
                blurImage.material = mat;
                Debug.LogError(focalLength+thickness*index+(yPoss2.y-yPos1.y));
                break;
            }

        }
    }
    public void PlaceGlassSlab(){
        glassSlab.SetActive(true);
        onStage.Add(glassSlab);
        CalculateImage();
    }
    public void PlaceImage(){
        plane.SetActive(true);
        Image crossImag = Instantiate(crossImage);
        crossImag.transform.SetParent(microscopeView.transform);
        crossImage.transform.position = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0);

        blurImage.transform.SetAsLastSibling();
        onStage.Add(plane);
        onStageView.Add(crossImag);
        
        CalculateImage();
    }
    public void PlaceSprinkle(){
        sprinkles.SetActive(true);
        onStage.Add(sprinkles);
        Image sprinklesImag = Instantiate(sprinklesImage);
        sprinklesImag.transform.SetParent(microscopeView.transform);
        crossImage.transform.position = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0);
        onStageView.Add(sprinklesImag);
        blurImage.transform.SetAsLastSibling();
        CalculateImage();
    }

  

    // Update is called once per frame
    void Update()
    {

    }
}

//focalLength+thickness*index+(yPoss2.y-yPos1.y)
