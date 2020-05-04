using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TravellingMicroscope : MonoBehaviour
{
    public float focalLength = 0.2f;
    List<GameObject> onStage = new List<GameObject>();
    GameObject underObservation;
    public Slider horizontal,vertical;
    public Text textHorizontal, textVertical;
    public GameObject horizontalSlider, verticalSlider, plane, glassSlab, sprinkles;

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
    
    // Start is called before the first frame update
    void Start()
    {
        PlaceImage();
    }

    void CalculateImage(){
        bool isGlassSlabPresent = false;
        float thickness = 0f;
        float index = 0f;
        for(int i = onStage.Count-1;i>=0;i--){
            GameObject gameObject = onStage[i];
            if(gameObject.name=="GlassSlab"){
                Debug.LogError("PREENT");
                isGlassSlabPresent = true;
                thickness = gameObject.GetComponent<GlassSlab>().thickness;
                index = gameObject.GetComponent<GlassSlab>().refractiveIndex;
                continue;

            }
            if(!isGlassSlabPresent){
                Vector3 yPos1 = verticalSlider.transform.position;
                Vector3 yPoss2 = plane.transform.position;
                gameObject.GetComponent<Cross>().updateBlur(0.9f+focalLength+thickness*index+(yPoss2.y-yPos1.y));
                Debug.LogError("SPRINL:E");
                Debug.LogError(0.9f+focalLength+thickness*index+(yPoss2.y-yPos1.y));
                
            }
            else{
                Vector3 yPos1 = verticalSlider.transform.position;
                Vector3 yPoss2 = plane.transform.position;
                gameObject.GetComponent<Cross>().updateBlur(0.9f+focalLength+thickness*index+(yPoss2.y-yPos1.y));
                Debug.LogError(0.9f+focalLength+focalLength+thickness*index+(yPoss2.y-yPos1.y));
            }

        }
    }
    public void PlaceGlassSlab(){
        glassSlab.SetActive(true);
        onStage.Add(glassSlab);
        CalculateImage();
    }
    public void PlaceImage(){
        onStage.Add(plane);
        CalculateImage();
    }
    public void PlaceSprinkle(){
        sprinkles.SetActive(true);
        onStage.Add(sprinkles);
        CalculateImage();
    }

  

    // Update is called once per frame
    void Update()
    {

    }
}
