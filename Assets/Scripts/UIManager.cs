using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject currentLowerDeck, currentUpperDeck;
    public GameObject previousLowerDeck, previousUpperDeck;
    public int optionSelected;
    // 1 = Mount
    //2 = Move

    //To check whether Image is spawned or not.
    public bool isObjectNeedlePlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        currentLowerDeck = null;
        currentUpperDeck = null;
        previousLowerDeck = null;
        previousUpperDeck = null;
        optionSelected = -1;

    }
    public void GoBack()
    {
        if (currentLowerDeck != null)
        {
            currentLowerDeck.SetActive(false);
        }
        if (currentUpperDeck != null)
        {
            currentUpperDeck.SetActive(false);
        }
        if (previousLowerDeck != null)
        {
            previousLowerDeck.SetActive(true);
            currentLowerDeck = previousLowerDeck;
        }
        if (previousUpperDeck != null)
        {
            previousUpperDeck.SetActive(true);
            currentUpperDeck = previousUpperDeck;
        }
        optionSelected = -1;

    }
    public void UpdateDecks(GameObject lowerDeck, GameObject upperDeck, int optionSelected1)
    {
        if(currentLowerDeck!=null){
            currentLowerDeck.SetActive(false);

        }
        if (currentUpperDeck != null)
        {
            currentUpperDeck.SetActive(false);

        }
        optionSelected = optionSelected1;
        previousLowerDeck = currentLowerDeck;
        previousUpperDeck = currentUpperDeck;
        currentUpperDeck = upperDeck;
        currentLowerDeck = lowerDeck;
        currentLowerDeck.SetActive(true);
        currentUpperDeck.SetActive(true);

    }
    public void UpdateDecks(GameObject upperDeck, int optionSelected1){
        optionSelected = optionSelected1;
        currentLowerDeck.SetActive(false);
        currentUpperDeck.SetActive(false);
        previousLowerDeck = currentLowerDeck;
        previousUpperDeck = currentUpperDeck;
        currentUpperDeck = upperDeck;
        currentUpperDeck.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
