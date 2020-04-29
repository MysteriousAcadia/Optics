using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvexLensNew : MonoBehaviour
{
    GameObject objectNeedle;
    GameObject objectScreen;
    

    [SerializeField] float focalLength = 2;

    [SerializeField] Text textNeedle;
    [SerializeField] Text textScreen;

    [SerializeField] Slider slider;
    [SerializeField] Slider sliderScreen;

    [SerializeField] GameObject image;
    [SerializeField] GameObject virImage;
    [SerializeField] GameObject opticalBench;

    //This will be in the scale
    //Infinity for object will be regarded as -100 and for image is 100
    float objectPos;
    float screenPos;

    float uValue = 3;
    float vValue = 3;
    float magnification;

    bool toDisplayImage = false;
    bool isPositionChanged = false;

    ScalePoints[] scalePoints;

    GameObject gameO;
    GameObject gameOVir;

    // Start is called before the first frame update
    void Start()
    {
        objectNeedle = FindObjectOfType<ObjectNeedle>().gameObject;
        objectScreen = FindObjectOfType<ObjectScreen>().gameObject;

        textNeedle.text = "4";
        textScreen.text = "4";

        gameO = Instantiate(image, new Vector3(4f, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z), Quaternion.identity, opticalBench.transform);

        gameOVir = Instantiate(virImage, new Vector3(-2000f, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z), Quaternion.identity, opticalBench.transform);

        
        gameO.transform.localPosition = new Vector3(-2 * focalLength, objectNeedle.transform.localPosition.y + 1.4f, objectNeedle.transform.localPosition.z);
        
        gameOVir.transform.Rotate(90, 0, 0);
        gameO.transform.Rotate(180, 0, 0);


    }

    // Update is called once per frame
    void Update()
    { 

        if (!isPositionChanged)
        {
            return;
        }

        uValue = objectPos;
        vValue = 0 - screenPos;

        textNeedle.text = uValue.ToString();
        textScreen.text = vValue.ToString();

        //Now start applying conditions for the lens, from here the script is variable

        //Object at infinity
        if (uValue == 1000)
        {
            gameO.transform.localPosition = new Vector3(focalLength, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
        }

        //Object behind C
        if (uValue > 2 * focalLength)
        {
            gameO.SetActive(true);

            float testVal;
            testVal =  1 / (-(1 / uValue) + (1 / focalLength));
            testVal = Mathf.Round(testVal * 10) / 10;
            

            magnification = -testVal / uValue;

            gameO.transform.localScale = new Vector3(gameO.transform.localScale.x, -1 * magnification, gameO.transform.localScale.z);

            gameO.transform.localPosition = new Vector3(-testVal, objectNeedle.transform.localPosition.y + 1.4f, objectNeedle.transform.localPosition.z);

           
            if (magnification < 0)
            {
                gameO.transform.localEulerAngles = new Vector3(180, 0, 0);
            }
            else
            {
                gameO.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            gameOVir.SetActive(false);

        }

        //Object at C
        if (uValue == 2 * focalLength)
        {
            gameO.SetActive(true);

            magnification = -2 * focalLength / uValue;
            
            gameO.transform.localScale = new Vector3(gameO.transform.localScale.x, -1 * magnification, gameO.transform.localScale.z);

            gameO.transform.localPosition = new Vector3(-2 * focalLength, objectNeedle.transform.localPosition.y + 1.4f , objectNeedle.transform.localPosition.z);

           
            
            if (magnification < 0)
            {
                gameO.transform.localEulerAngles = new Vector3(180, 0, 0);
            }
            else
            {
                gameO.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            gameOVir.SetActive(false);

        }

        //Object Between C and F
        if (uValue > focalLength && uValue < 2 * focalLength)
        {
            gameO.SetActive(true);

            float testVal;
            testVal = 1 / (-(1 / uValue) + (1 / focalLength));
            testVal = Mathf.Round(testVal * 10) / 10;

            magnification = -testVal / uValue;
            
            gameO.transform.localScale = new Vector3(gameO.transform.localScale.x, -1 * magnification, gameO.transform.localScale.z);

            gameO.transform.localPosition = new Vector3(-testVal, objectNeedle.transform.localPosition.y + 1.4f, objectNeedle.transform.localPosition.z);


            if (magnification < 0)
            {
                gameO.transform.localEulerAngles = new Vector3(180, 0, 0);
            }
            else
            {
                gameO.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            gameOVir.SetActive(false);
        }

        //Object at F
        if (uValue == focalLength)
        {
            gameO.SetActive(true);

            gameO.transform.localPosition = new Vector3(-1000f, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);

            gameOVir.SetActive(false);

        }

        //Object between F and O
        if (uValue < focalLength)
        {

            gameOVir.SetActive(true);

            float testVal;
            testVal = 1 / (-(1 / uValue) + (1 / focalLength));
            testVal = Mathf.Round(testVal * 10) / 10;

            magnification = -testVal / uValue;

            gameOVir.transform.localScale = new Vector3(gameOVir.transform.localScale.x, gameOVir.transform.localScale.y, magnification * 100);

            gameOVir.transform.localPosition = new Vector3(-testVal, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);

            
            if (magnification < 0)
            {
                gameOVir.transform.localEulerAngles = new Vector3(90, 0, 0);
            }
            else
            {
                gameOVir.transform.localEulerAngles = new Vector3(270, 0, 0);
            }

            gameO.SetActive(false);
        }

        
        isPositionChanged = false;
    }


    public void ChangeObjectPosition()
    {

        isPositionChanged = true;

        float newPos = 0f;

        newPos = slider.value;

        newPos = newPos * 5;

        if(newPos < 0.2)
        {
            newPos = 0.2f;
        }

        if (newPos > 5)
        {
            newPos = 5f;
        }

        newPos = (Mathf.Round(newPos * 10)) / 10;

        objectNeedle.transform.localPosition = new Vector3(newPos, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
        objectPos = newPos;
    }

    public void ChangeScreenPosition()
    {

        isPositionChanged = true;

        float newPos = 0f;

        newPos = 1 - sliderScreen.value;

        newPos = newPos * 5;

        newPos = newPos - 5;

        if (newPos > -0.2)
        {
            newPos = -0.2f;
        }

        if (newPos < -5)
        {
            newPos = -5f;
        }

        newPos = (Mathf.Round(newPos * 10)) / 10;

        objectScreen.transform.localPosition = new Vector3(newPos, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
        screenPos = newPos;
    }
}