using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpticalBenchPlace : MonoBehaviour
{
    public GameObject convexMirror; // Used only in FL of convex mirror experiment
    public GameObject concaveLens;// Used only in FL of concaveLens experiment
    public ConvexLensNew convexLensNew;
    public GameObject convexLens;
    public GameObject concaveMirror;//Used to 
    public GameObject objectNeedle;//Used in all experiments
    public GameObject imageNeedle;//Used in all experiments
    public bool isPlaced = false;
    public GameObject upperDeckGameO, lowerDeckGameO;
    public GameObject upperDeckMountGameO;
    public GameObject concaveMirrorSlider, objectSlider, ImageSlider;
    public GameObject convexLensSlider,concaveLensSlider,convexMirrorSlider;
    Animator upperDeck, lowerDeck, upperDeckMount;
    public Text t;
    bool isSearchingforObjects = false;
    public UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        if (uIManager == null)
        {
            Debug.LogError("OpticalBench: UI Manager is missing!");
        }
        if (upperDeckGameO == null)
        {
            Debug.LogError("OpticalBench: UpperDeckGameO is missing!");
        }
        upperDeck = upperDeckGameO.GetComponent<Animator>();
        lowerDeck = lowerDeckGameO.GetComponent<Animator>();
        Input.simulateMouseWithTouches = true;
    }

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

    bool islol = true;
    // Update is called once per frame
    void Update()
    {
        if (isSearchingforObjects)
        {
            if (uIManager.optionSelected == 1)
            {
                if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("This hit at " + hit.point);
                        if (hit.collider != null)
                        {
                            GameObject touchedObject = hit.transform.gameObject;
                            if (touchedObject.transform.name == "NeedleShow1" || touchedObject.transform.name == "NeedleShow2")
                            {
                                touchedObject.SetActive(false);
                                if (uIManager.isObjectNeedlePlaced)
                                {
                                    PlaceImageNeedle();

                                }
                                else
                                {
                                    PlaceObjectNeedle();
                                    uIManager.isObjectNeedlePlaced = true;
                                }
                                uIManager.GoBack();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "ConcaveMirrorShow")
                            {
                                touchedObject.SetActive(false);
                                placeConcaveMirror();
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
                            if (touchedObject.transform.name == "NeedleShow1" || touchedObject.transform.name == "NeedleShow2")
                            {
                                touchedObject.SetActive(false);
                                if (uIManager.isObjectNeedlePlaced)
                                {
                                    PlaceImageNeedle();

                                }
                                else
                                {
                                    PlaceObjectNeedle();
                                    uIManager.isObjectNeedlePlaced = true;
                                }
                                uIManager.GoBack();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "ConcaveMirrorShow")
                            {
                                touchedObject.SetActive(false);
                                placeConcaveMirror();
                                uIManager.GoBack();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "ConvexMirrorShow")
                            {
                                touchedObject.SetActive(false);
                                placeConvexMirror();
                                uIManager.GoBack();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "ConvexLensShow")
                            {
                                touchedObject.SetActive(false);
                                placeConvexLens();
                                uIManager.GoBack();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "ConcaveLensShow")
                            {
                                touchedObject.SetActive(false);
                                placeConcaveLens();
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
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("This hit at " + hit.point);
                        if (hit.collider != null)
                        {
                            GameObject touchedObject = hit.transform.gameObject;
                            if (touchedObject.transform.name == "ConcaveMirror")
                            {
                                DisplayConcaveMirrorSlider();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "Needle")
                            {
                                DisplayObjectNeedleSlider();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "Screen")
                            {
                                DisplayImageNeedleSlider();
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
                            if (touchedObject.transform.name == "ConcaveMirror")
                            {
                                DisplayConcaveMirrorSlider();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "Needle")
                            {
                                DisplayObjectNeedleSlider();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "Screen")
                            {
                                DisplayImageNeedleSlider();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "ConvexMirror")
                            {
                                DisplayConvexMirrorSlider();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "ConcaveLens")
                            {
                                DisplayConcaveLensSlider();
                                isSearchingforObjects = false;
                            }
                            else if (touchedObject.transform.name == "ConvexLens")
                            {
                                DisplayConvexLensSlider();
                                isSearchingforObjects = false;
                            }
                            
                            Debug.Log("Touched " + touchedObject.transform.name);
                        }

                    }

                }
            }
        }

    }
    public void DisplayConcaveMirrorSlider()
    {
        uIManager.UpdateDecks(concaveMirrorSlider, upperDeckGameO, 2);
        uIManager.previousLowerDeck = lowerDeckGameO;
        uIManager.previousUpperDeck = upperDeckGameO;

    }
    public void DisplayImageNeedleSlider()
    {
        uIManager.UpdateDecks(ImageSlider, upperDeckGameO, 2);
        uIManager.previousLowerDeck = lowerDeckGameO;
        uIManager.previousUpperDeck = upperDeckGameO;
    }
    public void DisplayObjectNeedleSlider()
    {
        uIManager.UpdateDecks(objectSlider, upperDeckGameO, 2);
        uIManager.previousLowerDeck = lowerDeckGameO;
        uIManager.previousUpperDeck = upperDeckGameO;
    }
    public void DisplayConvexLensSlider()
    {
        uIManager.UpdateDecks(convexLensSlider, upperDeckGameO, 2);
        uIManager.previousLowerDeck = lowerDeckGameO;
        uIManager.previousUpperDeck = upperDeckGameO;
    }
    public void DisplayConvexMirrorSlider()
    {
        uIManager.UpdateDecks(convexMirrorSlider, upperDeckGameO, 2);
        uIManager.previousLowerDeck = lowerDeckGameO;
        uIManager.previousUpperDeck = upperDeckGameO;
    }
    public void DisplayConcaveLensSlider()
    {
        uIManager.UpdateDecks(concaveLensSlider, upperDeckGameO, 2);
        uIManager.previousLowerDeck = lowerDeckGameO;
        uIManager.previousUpperDeck = upperDeckGameO;
    }
    public void PlaceImageNeedle()
    {
        imageNeedle.SetActive(true);
    }
    public void PlaceObjectNeedle()
    {
        objectNeedle.SetActive(true);
    }
    public void RemoveImageNeedle()
    {
        imageNeedle.SetActive(false);
    }
    public void RemoveObjectNeedle()
    {
        objectNeedle.SetActive(false);
    }
    public void placeConcaveMirror()
    {
        concaveMirror.SetActive(true);

    }
    public void toggleConcaveLens()
    {
        if (!isPlaced)
        {
            concaveLens.SetActive(true);
            convexLensNew.isLensPlaced = true;
            convexLensNew.concaveLens = concaveLens.GetComponent<ConcaveLens>();
            convexLensNew.isPositionChanged = true;


        }
        else
        {
            concaveLens.SetActive(false);
            convexLensNew.isLensPlaced = false;
            convexLensNew.concaveLens = null;
            convexLensNew.isPositionChanged = true;

        }
        isPlaced = !isPlaced;
    }
    public void toggleConvexMirror()
    {
        if (!isPlaced)
        {
            convexMirror.SetActive(true);
            convexLensNew.isMirrorPlaced = true;
            convexLensNew.convexMirror = convexMirror.GetComponent<ConvexMirror>();
            convexLensNew.isPositionChanged = true;


        }
        else
        {
            convexMirror.SetActive(false);
            convexLensNew.isMirrorPlaced = false;
            convexLensNew.convexMirror = null;
            convexLensNew.isPositionChanged = true;

        }
        isPlaced = !isPlaced;

    }
    public void placeConvexMirror()
    {
        convexMirror.SetActive(true);
        convexLensNew.isMirrorPlaced = true;
        convexLensNew.convexMirror = convexMirror.GetComponent<ConvexMirror>();
        convexLensNew.isPositionChanged = true;
        isPlaced = true;

    }
    public void addConvexMirror()
    {
        convexMirror.SetActive(true);
        convexLensNew.isMirrorPlaced = true;
        convexLensNew.convexMirror = convexMirror.GetComponent<ConvexMirror>();
        convexLensNew.isPositionChanged = true;
        isPlaced = true;

    }
    public void removeConvexMirror()
    {
        convexMirror.SetActive(false);
        convexLensNew.isMirrorPlaced = false;
        convexLensNew.convexMirror = null;
        convexLensNew.isPositionChanged = true;
        isPlaced = false;

    }
    public void placeConcaveLens()
    {
        concaveLens.SetActive(true);
        convexLensNew.isLensPlaced = true;
        convexLensNew.concaveLens = concaveLens.GetComponent<ConcaveLens>();
        convexLensNew.isPositionChanged = true;
        isPlaced = true;

    }
    public void addConcaveLens()
    {
        concaveLens.SetActive(true);
        convexLensNew.isLensPlaced = true;
        convexLensNew.concaveLens = concaveLens.GetComponent<ConcaveLens>();
        convexLensNew.isPositionChanged = true;
        isPlaced = true;

    }
    public void removeConcaveLens()
    {
        concaveLens.SetActive(false);
        convexLensNew.isLensPlaced = false;
        convexLensNew.concaveLens = null;
        convexLensNew.isPositionChanged = true;
        isPlaced = false;
    }
    public void placeConvexLens(){
        convexLens.SetActive(true);
    }
    public void AdjustApparatus()
    {
        uIManager.UpdateDecks(upperDeckGameO, 3);
        isSearchingforObjects = true;
    }

}
