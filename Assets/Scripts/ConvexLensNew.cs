﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvexLensNew : MonoBehaviour
{
    public GameObject objectNeedle;
    public GameObject objectScreen;

    public ConvexMirror convexMirror;
    public ConcaveLens concaveLens;

    [SerializeField] public bool isMirrorPlaced = false;
    [SerializeField] public bool isLensPlaced = true;

    [SerializeField] public float focalLength = 1f;

    [SerializeField] Text textNeedle;
    [SerializeField] Text textScreen;
    [SerializeField] Text textConvexLens;


    [SerializeField] Slider objectSlider;
    [SerializeField] Slider sliderScreen;
    [SerializeField] Slider sliderConvexLens;

    [SerializeField] GameObject image;
    [SerializeField] GameObject virImage;
    [SerializeField] GameObject opticalBench;

    //This will be in the scale
    //Infinity for object will be regarded as -100 and for image is 100
    float objectPos;
    float screenPos;
    public bool imageVisible = false;

    float uValue = 2;
    float vValue = 2;
    float magnification;

    bool toDisplayImage = false;
    public bool isPositionChanged = false;

    ScalePoints[] scalePoints;

    public GameObject gameO;
    GameObject gameOVir;

    // Start is called before the first frame update
    void Start()
    {
        gameO = Instantiate(image, new Vector3(4f, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z), Quaternion.identity, opticalBench.transform);

        gameOVir = Instantiate(virImage, new Vector3(-2000f, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z), Quaternion.identity, opticalBench.transform);

        gameO.SetActive(false);
        gameOVir.SetActive(false);
        gameO.transform.localPosition = new Vector3(-2 * focalLength, objectNeedle.transform.localPosition.y + 1.05f, objectNeedle.transform.localPosition.z);

        gameOVir.transform.Rotate(90, 0, 0);
        gameO.transform.Rotate(180, 0, 0);


    }
    float oVal = 4f;

    // Update is called once per frame
    void Update()
    {

        if (!isPositionChanged)
        {
            return;
        }

        uValue = objectPos - gameObject.transform.localPosition.x;
        vValue = 0 - screenPos;

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
            testVal = 1 / (-(1 / uValue) + (1 / focalLength));
            testVal = Mathf.Round(testVal * 10) / 10;


            magnification = -testVal / uValue;

            gameO.transform.localScale = new Vector3(gameO.transform.localScale.x, -1 * magnification, gameO.transform.localScale.z);

            gameO.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - testVal, objectNeedle.transform.localPosition.y + 1.05f, objectNeedle.transform.localPosition.z);


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

            gameO.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - 2 * focalLength, objectNeedle.transform.localPosition.y + 1.05f, objectNeedle.transform.localPosition.z);



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

            gameO.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - testVal, objectNeedle.transform.localPosition.y + 1.05f, objectNeedle.transform.localPosition.z);


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

            // gameOVir.transform.localScale = new Vector3(gameOVir.transform.localScale.x, gameOVir.transform.localScale.y, magnification * 100);
            gameOVir.transform.localScale = new Vector3(gameOVir.transform.localScale.x, magnification, gameOVir.transform.localScale.z);

            gameOVir.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - testVal, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
            gameOVir.SetActive(true);


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

        if (!imageVisible)
        {
            gameO.SetActive(false);
            gameOVir.SetActive(false);
        }
        //Part for calculating resultant image with convex mirror.
        if (isMirrorPlaced)
        {
            if (convexMirror == null)
            {
                // gameO.transform.localPosition = new Vector3(-gameO.transform.localPosition.x,gameO.transform.localPosition.y,gameO.transform.localPosition.z);
            }
            else
            {
                float distBetweenLens = convexMirror.transform.localPosition.x - gameObject.transform.localPosition.x;
                // float ultimateimgPos = distBetweenLens-1.5f;
                // float actualObjectPos = 1/((1/-focalLength)+(1/ultimateimgPos));
                Debug.LogError(distBetweenLens + "LENS");
                Debug.LogError(gameO.transform.localPosition.x + "IMG");
                float objectPoss = -gameO.transform.localPosition.x + distBetweenLens;
                Debug.LogError(objectPoss + "u(should be opp)");
                float imagePos = distBetweenLens - (1 / ((1 / convexMirror.focalLenght) - (1 / objectPoss)));
                Debug.LogError(imagePos + "imgpos");
                float finalImagePos = 1 / ((1 / (imagePos)) + (1 / focalLength));
                gameO.transform.localPosition = new Vector3(finalImagePos + gameObject.transform.localPosition.x, gameO.transform.localPosition.y, gameO.transform.localPosition.z);

            }
        }

        //To find Resultant image with concave lens.
        if (isLensPlaced)
        {
            if (concaveLens != null)
            {
                float distBetweenLens = concaveLens.transform.localPosition.x - gameObject.transform.localPosition.x;
                // float ultimateimgPos = distBetweenLens-1.5f;
                // float actualObjectPos = 1/((1/-focalLength)+(1/ultimateimgPos));
                Debug.LogError(distBetweenLens + "LENS");
                Debug.LogError(gameO.transform.localPosition.x + "IMG");
                float objectPoss = gameO.transform.localPosition.x - distBetweenLens;
                Debug.LogError(objectPoss + "u(should be opp)");
                float imagePos = distBetweenLens + (1 / ((1 / -1f) - (1 / objectPoss)));
                // float imagePos = distBetweenLens+(-2*objectPoss)/(-2+objectPoss);
                Debug.LogError(imagePos + "imgpos");
                // float finalImagePos = 1 / ((1 / (imagePos)) + (1 /focalLength));
                gameO.transform.localPosition = new Vector3(imagePos-gameObject.transform.localPosition.x, gameO.transform.localPosition.y, gameO.transform.localPosition.z);

            }
        }

        isPositionChanged = false;
    }

    public void ChangeConvexLensPosition()
    {
        isPositionChanged = true;

        float newPos = 0f;

        newPos = sliderConvexLens.value;

        newPos = newPos * 10;
        newPos = 5 - newPos;
        if (objectNeedle.transform.localPosition.x - newPos < 0.5f)
        {
            newPos = objectNeedle.transform.localPosition.x - 0.5f;
            sliderConvexLens.value = (5 - newPos) / 10f;

        }
        if (newPos - objectScreen.transform.localPosition.x < 0.5f)
        {
            newPos = objectScreen.transform.localPosition.x + 0.5f;
            sliderConvexLens.value = (5 - newPos) / 10f;
        }

        newPos = (Mathf.Round(newPos * 10)) / 10;
        gameObject.transform.localPosition = new Vector3(newPos, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        textConvexLens.text = ((5 - newPos) * 10f).ToString();

    }


    public void ChangeObjectPosition()
    {

        isPositionChanged = true;

        float newPos = 0f;

        newPos = objectSlider.value;

        newPos = newPos * 10;
        newPos = 5 - newPos;
        if (newPos - gameObject.transform.localPosition.x < 0.5f)
        {
            newPos = gameObject.transform.localPosition.x + 0.5f;
            objectSlider.value = (5 - newPos) / 10f;
        }

        newPos = (Mathf.Round(newPos * 10)) / 10;
        objectNeedle.transform.localPosition = new Vector3(newPos, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
        objectPos = newPos;
        textNeedle.text = ((5 - newPos) * 10f).ToString();
    }

    public void ChangeScreenPosition()
    {

        isPositionChanged = true;

        float newPos = sliderScreen.value;

        newPos = newPos * 10;

        newPos = 5 - newPos;
        if (isLensPlaced)
        {
            if (concaveLens.gameObject.transform.localPosition.x - newPos < 0.5f)
            {
                newPos = gameObject.transform.localPosition.x - 0.5f;
                sliderScreen.value = (5 - newPos) / 10f;
            }
        }
        else if (isMirrorPlaced)
        {
            if (convexMirror.gameObject.transform.localPosition.x - newPos < 0.5f)
            {
                newPos = gameObject.transform.localPosition.x - 0.5f;
                sliderScreen.value = (5 - newPos) / 10f;
            }
        }
        else
        {
            if (gameObject.transform.localPosition.x - newPos < 0.5f)
            {
                newPos = gameObject.transform.localPosition.x - 0.5f;
                sliderScreen.value = (5 - newPos) / 10f;
            }
        }


        newPos = (Mathf.Round(newPos * 10)) / 10;
        objectScreen.transform.localPosition = new Vector3(newPos, objectNeedle.transform.localPosition.y, objectNeedle.transform.localPosition.z);
        textScreen.text = ((5 - newPos) * 10f).ToString();
    }

    public void switchView()
    {
        imageVisible = !imageVisible;
        isPositionChanged = true;
    }
}
