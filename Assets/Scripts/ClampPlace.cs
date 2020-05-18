using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPlace : MonoBehaviour
{
    public UIManager uIManager;
    public GameObject upperDeckGameO,lowerDeckGameO, upperDeckMountGameO;
    public GameObject objectSlider;
    public GameObject objectNeedle;
    public bool isSearchingforObjects = false;
    // Start is called before the first frame update
    void OnMouseDown()
    {
        if (uIManager.optionSelected < 0)
        {
            uIManager.UpdateMenu(lowerDeckGameO, upperDeckGameO, 4);
        }
    }

    public void Mount()
    {
        uIManager.UpdateDecks(upperDeckMountGameO, 1);
        isSearchingforObjects = true;
    }
    void Update()
    {
        if (isSearchingforObjects)
        {
            if (uIManager.optionSelected == 1)
            {
                if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    // Casts the ray and get the first game object hit
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("This hit at " + hit.point);
                        if (hit.collider != null)
                        {
                            GameObject touchedObject = hit.transform.gameObject;
                            if (touchedObject.transform.name == "NeedleShow")
                            {
                                touchedObject.SetActive(false);

                                PlaceObjectNeedle();

                                uIManager.GoBack();
                                isSearchingforObjects = false;
                            }


                            Debug.Log("Touched " + touchedObject.transform.name);
                        }

                    }
                }
                else if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    // Casts the ray and get the first game object hit
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("This hit at " + hit.point);
                        if (hit.collider != null)
                        {
                            GameObject touchedObject = hit.transform.gameObject;
                            if (touchedObject.transform.name == "NeedleShow")
                            {
                                touchedObject.SetActive(false);
                        
                                PlaceObjectNeedle();
                                  
                                uIManager.GoBack();
                                isSearchingforObjects = false;
                            }
                            

                            Debug.Log("Touched " + touchedObject.transform.name);
                        }

                    }

                }
            }
            else if (uIManager.optionSelected == 3)
            {
                if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    // Casts the ray and get the first game object hit
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("This hit at " + hit.point);
                        if (hit.collider != null)
                        {
                            GameObject touchedObject = hit.transform.gameObject;
                            if (touchedObject.transform.name == "Needle")
                            {
                                DisplayObjectNeedleSlider();
                                isSearchingforObjects = false;
                            }

                            Debug.Log("Touched " + touchedObject.transform.name);
                        }

                    }
                }
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    // Casts the ray and get the first game object hit
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("This hit at " + hit.point);
                        if (hit.collider != null)
                        {
                            GameObject touchedObject = hit.transform.gameObject;
                            if (touchedObject.transform.name == "Needle")
                            {
                                DisplayObjectNeedleSlider();
                                isSearchingforObjects = false;
                            }

                            Debug.Log("Touched " + touchedObject.transform.name);
                        }

                    }

                }
            }
        }
    }
    public void DisplayObjectNeedleSlider()
    {
        uIManager.UpdateDecks(objectSlider, upperDeckGameO, 2);
        uIManager.previousLowerDeck = lowerDeckGameO;
        uIManager.previousUpperDeck = upperDeckGameO;
    }
    public void PlaceObjectNeedle()
    {
        objectNeedle.SetActive(true);
    }
    public void AdjustApparatus()
    {
        uIManager.UpdateDecks(upperDeckGameO, 3);
        isSearchingforObjects = true;
    }
}
