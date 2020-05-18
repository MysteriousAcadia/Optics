using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvexMirrorWater : MonoBehaviour
{
    public GameObject objectNeedle;
    

    private float focalLength = 3f;
    [SerializeField] float lensFocalLength = 3f;
    [SerializeField] float liquidIndec = 1.33f;

    [SerializeField] Text textNeedle;
    [SerializeField] Text textScreen;

    [SerializeField] Slider slider;

    [SerializeField] GameObject image;
    [SerializeField] GameObject virImage;
    [SerializeField] GameObject opticalBench;

    //This will be in the scale
    //Infinity for object will be regarded as -100 and for image is 100
    float objectPos;

    float uValue = 0.1f;
    float vValue = 0.1f;
    float magnification;

    public bool isPositionChanged = false;

    GameObject gameO;
    GameObject gameOVir;
    bool isWaterPresent = false;
    public bool isToggleOn = false;

    // Start is called before the first frame update
    void Start()
    {

        textNeedle.text = "4";

        gameO = Instantiate(image, new Vector3(objectNeedle.transform.localPosition.y, 4f, objectNeedle.transform.localPosition.z), Quaternion.Euler(new Vector3(0, 90, 90)), opticalBench.transform);
        gameO.transform.localScale = new Vector3(1,1,1);
        focalLength = lensFocalLength/2;
        gameOVir = Instantiate(virImage, new Vector3(-2000f, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z), Quaternion.identity, opticalBench.transform);
        isToggleOn = false;
        
        gameO.transform.localPosition = new Vector3(objectNeedle.transform.localPosition.x, 2 * focalLength, objectNeedle.transform.localPosition.z);
        gameO.transform.localEulerAngles = (new Vector3(-90,0,0));
            gameO.transform.localPosition =new Vector3(gameO.transform.localPosition.x, vValue,0.87f);
        gameOVir.transform.Rotate(0, 0, 90);
        gameOVir.SetActive(false);
        gameO.SetActive(false);
        // gameO.transform.Rotate(0, 90, 180);


    }
    bool isActive = true;
    public void SetActive(bool isActive1){
        this.isActive = isActive1;
    }
    public void ToggleView(){
        isPositionChanged = true;
        isToggleOn = !isToggleOn;
    }
    

    // Update is called once per frame
    void Update()
    { 
        if(!isActive){
            gameO.SetActive(false);
            gameOVir.SetActive(false);
            return;
        }

        if (!isPositionChanged)
        {
            return;
        }


        uValue = objectPos;

        textNeedle.text = (uValue*10).ToString();
        if(focalLength==uValue){
            Debug.LogError("INFINITY");
            gameO.SetActive(false);
        }
        else if(uValue<focalLength){
            Debug.LogError("Virtual");
            gameO.SetActive(false);

            // vValue = -1/((1/focalLength)-(1/uValue));
            //             magnification = vValue/uValue;
            // gameO.transform.localEulerAngles = (new Vector3(-180,0,0));
            // gameO.transform.localPosition =new Vector3(gameO.transform.localPosition.x, vValue,0f);
            // gameO.transform.localScale = new Vector3(gameO.transform.localScale.x,magnification,gameO.transform.localScale.z);


        }
        else{
            gameO.SetActive(true);
            vValue = 1/((1/focalLength)-(1/uValue));
            magnification = vValue/uValue;
            gameO.transform.localEulerAngles = (new Vector3(-90,0,0));
            gameO.transform.localPosition =new Vector3(gameO.transform.localPosition.x, vValue,1.51f);
            gameO.transform.localScale = new Vector3(gameO.transform.localScale.x,magnification,gameO.transform.localScale.z);
        }

        //Now start applying conditions for the lens, from here the script is variable

        //Object at infinity
        // if (uValue == 1000)
        // {
        //     gameO.transform.localPosition = new Vector3(objectNeedle.transform.localPosition.x, focalLength, objectNeedle.transform.localPosition.z);
        // }

        // //Object behind C
        // if (uValue > 2 * focalLength)
        // {
        //     gameO.SetActive(true);

        //     float testVal;
        //     testVal =  1 / ((1 / uValue) + (1 / focalLength));
        //     testVal = Mathf.Round(testVal * 10) / 10;
            

        //     magnification = testVal / uValue;

        //     gameO.transform.localScale = new Vector3(gameO.transform.localScale.x, magnification, gameO.transform.localScale.z);

        //     gameO.transform.localPosition = new Vector3(objectNeedle.transform.localPosition.x, testVal,objectNeedle.transform.localPosition.z);

           
        //     if (magnification < 0)
        //     {
        //         gameO.transform.localEulerAngles = new Vector3(0, 0, 180);
        //     }
        //     else
        //     {
        //         gameO.transform.localEulerAngles = new Vector3(0, 0, 0);
        //     }

        //     gameOVir.SetActive(false);

        // }

        // //Object at C
        // if (uValue == 2 * focalLength)
        // {
        //     gameO.SetActive(true);

        //     magnification = -2 * focalLength / uValue;
            
        //     gameO.transform.localScale = new Vector3(gameO.transform.localScale.x, -1 * magnification, gameO.transform.localScale.z);

        //     gameO.transform.localPosition = new Vector3(objectNeedle.transform.localPosition.x,2 * focalLength , objectNeedle.transform.localPosition.z);

           
            
        //     if (magnification < 0)
        //     {
        //         gameO.transform.localEulerAngles = new Vector3(0, 0, 180);
        //     }
        //     else
        //     {
        //         gameO.transform.localEulerAngles = new Vector3(0, 0, 0);
        //     }

        //     gameOVir.SetActive(false);

        // }

        // //Object Between C and F
        // if (uValue > focalLength && uValue < 2 * focalLength)
        // {
        //     gameO.SetActive(true);

        //     float testVal;
        //     testVal = 1 / ((1 / uValue) + (1 / focalLength));
        //     testVal = Mathf.Round(testVal * 10) / 10;

        //     magnification = -testVal / uValue;
            
        //     gameO.transform.localScale = new Vector3(gameO.transform.localScale.x,  magnification, gameO.transform.localScale.z);

        //     gameO.transform.localPosition = new Vector3(objectNeedle.transform.localPosition.x,testVal , objectNeedle.transform.localPosition.z);


        //     if (magnification < 0)
        //     {
        //         gameO.transform.localEulerAngles = new Vector3(180, 0, 0);
        //     }
        //     else
        //     {
        //         gameO.transform.localEulerAngles = new Vector3(0, 0, 0);
        //     }

        //     gameOVir.SetActive(false);
        // }

        // //Object at F
        // if (uValue == focalLength)
        // {
        //     gameO.SetActive(true);

        //     gameO.transform.localPosition = new Vector3(objectNeedle.transform.localPosition.x, 1000f, objectNeedle.transform.localPosition.z);

        //     gameOVir.SetActive(false);

        // }

        // //Object between F and O
        // if (uValue < focalLength)
        // {

        //     gameOVir.SetActive(true);

        //     float testVal;
        //     testVal = 1 / ((1 / uValue) + (1 / focalLength));
        //     testVal = Mathf.Round(testVal * 10) / 10;

        //     magnification = -testVal / uValue;

        //     gameOVir.transform.localScale = new Vector3(gameOVir.transform.localScale.x, gameOVir.transform.localScale.y, magnification * 100);

        //     gameOVir.transform.localPosition = new Vector3(objectNeedle.transform.localPosition.x, testVal, objectNeedle.transform.localPosition.z);

            
        //     if (magnification < 0)
        //     {
        //         gameOVir.transform.localEulerAngles = new Vector3(0, 0, 90);
        //     }
        //     else
        //     {
        //         gameOVir.transform.localEulerAngles = new Vector3(0, 0, 270);
        //     }

        //     gameO.SetActive(false);
        // }

        
        isPositionChanged = false;
        if (!isToggleOn)
        {
            gameO.SetActive(false);
            gameOVir.SetActive(false);
            return;
        }
    }

    public void SwitchLiquid(float rf){
        gameO.SetActive(false);
        if(rf==1f){
            gameO.SetActive(false);
            // textScreen.text = "CLick to add Water";
            focalLength = lensFocalLength/2;
            
        }
        else{
            // textScreen.text = "CLick to remove Water";
            float newlensFocalLength = -2*lensFocalLength/0.33f;
            focalLength = 3f*newlensFocalLength/(3f+newlensFocalLength);
            focalLength = focalLength/2;
        }
        isWaterPresent = !isWaterPresent;
    }


    public void ChangeObjectPosition()
    {

        isPositionChanged = true;

        float newPos = 0f;

        newPos = slider.value;

        newPos = newPos * 10;

        if(newPos < 0.02)
        {
            newPos = 0.2f;
        }

        if (newPos > 10.1)
        {
            newPos = 5f;
        }

        newPos = (Mathf.Round(newPos * 100)) / 100;

        objectNeedle.transform.localPosition = new Vector3(objectNeedle.transform.localPosition.x,newPos , objectNeedle.transform.localPosition.z);
        objectPos = newPos;
    }

   
}