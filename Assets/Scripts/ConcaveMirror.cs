using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcaveMirror : MonoBehaviour
{
    public
    GameObject objectNeedle;
    public GameObject objectScreen;


    [SerializeField] public float focalLength = 2;

    [SerializeField] Text textNeedle;
    [SerializeField] Text textScreen;

    [SerializeField]Text mirrorText;

    [SerializeField] Slider objectSlider;
    [SerializeField] Slider concaveMirrorSlider;
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

    bool isPositionChanged = false;
    public bool imageVisible = false;

    ScalePoints[] scalePoints;

    GameObject gameO;
    GameObject gameOVir;

    // Start is called before the first frame update
    void Start()
    {
        textNeedle.text = "4";
        textScreen.text = "4";

        gameO = Instantiate(image, new Vector3(4f, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z), Quaternion.identity, opticalBench.transform);

        gameOVir = Instantiate(virImage, new Vector3(-2000f, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z), Quaternion.identity, opticalBench.transform);


        gameO.transform.localPosition = new Vector3(-2 * focalLength, objectNeedle.transform.localPosition.y + 1f, objectNeedle.transform.localPosition.z);

        gameOVir.transform.Rotate(90, 0, 0);
        gameO.transform.Rotate(180, 0, 0);


    }

    // Update is called once per frame
    void Update()
    {
        if (!imageVisible || !gameObject.activeSelf || !objectNeedle.activeSelf)
        {
            gameO.SetActive(false);
            gameOVir.SetActive(false);
        }

        if (!isPositionChanged)
        {
            return;
        }

        uValue = 0f - objectPos;
        vValue = 0f - screenPos;

    
        //Now start applying conditions for the lens, from here the script is variable

        //Object at infinity
        if (uValue == 1000)
        {
            // gameO.transform.localPosition = new Vector3(focalLength, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
        }

        //Object behind C
        if (uValue > 2 * focalLength)
        {
            gameO.SetActive(true);

            float testVal;
            testVal = 1 / (-(1 / uValue) + (1 / focalLength));
            testVal = Mathf.Round(testVal * 10) / 10;


            magnification = -testVal / uValue;

            gameO.transform.localScale = new Vector3(gameO.transform.localScale.x, -1 * magnification, gameO.transform.localScale.z);

            gameO.transform.localPosition = new Vector3(-testVal + gameObject.transform.localPosition.x, objectNeedle.transform.localPosition.y + 1f, objectNeedle.transform.localPosition.z);


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

            gameO.transform.localPosition = new Vector3((-2 * focalLength) + gameObject.transform.localPosition.x, objectNeedle.transform.localPosition.y + 1f, objectNeedle.transform.localPosition.z);



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

            gameO.transform.localPosition = new Vector3(-testVal + gameObject.transform.localPosition.x, objectNeedle.transform.localPosition.y + 1f, objectNeedle.transform.localPosition.z);


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

            // gameOVir.transform.localScale = new Vector3(gameOVir.transform.localScale.x, gameOVir.transform.localScale.y, gameOVir.transform.localScale.z);
            gameOVir.transform.localScale = new Vector3(gameOVir.transform.localScale.x, magnification, gameOVir.transform.localScale.z);

            gameOVir.transform.localPosition = new Vector3(-testVal + gameObject.transform.localPosition.x, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
            // gameOVir.transform.localPosition = new Vector3(-testVal, magnification, gameOVir.transform.localPosition.z);



            // if (magnification < 0)
            // {
            gameOVir.transform.localEulerAngles = new Vector3(0, 0, 0);
            // }
            // else
            // {
            //     gameOVir.transform.localEulerAngles = new Vector3(270, 0, 0);
            // }

            gameO.SetActive(false);
        }
        if (!imageVisible || !gameObject.activeSelf || !objectNeedle.activeSelf)
        {
            gameO.SetActive(false);
            gameOVir.SetActive(false);
        }


        isPositionChanged = false;
    }

    public void ChangeMirrorPosition()
    {
        isPositionChanged = true;

        float newPos = 0f;

        newPos = concaveMirrorSlider.value;

        newPos = newPos * 10;

        newPos = 5 - newPos;
        float closestNeedle = objectNeedle.transform.localPosition.x>objectScreen.transform.localPosition.x ?objectNeedle.transform.localPosition.x :objectScreen.transform.localPosition.x;
        if ((newPos-closestNeedle) < 0.5f)
        {
            newPos = closestNeedle + 0.5f;
            concaveMirrorSlider.value = (5 - newPos) / 10f;
        }

        Debug.LogError(closestNeedle.ToString() + " " + newPos.ToString());

        newPos = ((Mathf.Round(newPos * 10)) / 10);

        gameObject.transform.localPosition = new Vector3(newPos, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        // objectPos = -gameObject.transform.localPosition.x + newPos;   
        mirrorText.text = ((5 - newPos) * 10f).ToString();

    }



    public void ChangeObjectPosition()
    {

        isPositionChanged = true;

        float newPos = 0f;

        newPos = objectSlider.value;

        newPos = newPos * 10;

        newPos = 5 - newPos;
        if ((gameObject.transform.localPosition.x - newPos) < 0.5f)
        {
            newPos = gameObject.transform.localPosition.x - 0.5f;
            objectSlider.value = (5 - newPos) / 10f;
        }

        Debug.LogError(newPos.ToString() + " " + screenPos.ToString());

        newPos = ((Mathf.Round(newPos * 10)) / 10);

        objectNeedle.transform.localPosition = new Vector3(newPos, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
        objectPos = -gameObject.transform.localPosition.x + newPos;
        textNeedle.text = ((5-newPos) * 10f).ToString() ;

    }

    public void ChangeScreenPosition()
    {

        isPositionChanged = true;

        float newPos = 0f;

        newPos = sliderScreen.value;

        newPos = newPos * 10;

        newPos = 5 - newPos;

        newPos = (Mathf.Round(newPos * 10)) / 10;
        if ((gameObject.transform.localPosition.x - newPos) < 0.5f)
        {
            newPos = gameObject.transform.localPosition.x - 0.5f;
            sliderScreen.value = (5 - newPos) / 10f;
        }

        objectScreen.transform.localPosition = new Vector3(newPos, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
        screenPos = -gameObject.transform.localPosition.x + newPos;
        textScreen.text = ((5 - newPos) * 10f).ToString();

    }

    public void switchView()
    {
        imageVisible = !imageVisible;
        isPositionChanged = true;
    }
}