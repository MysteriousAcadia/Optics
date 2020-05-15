using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject currentLowerDeck,currentUpperDeck;
    public GameObject previousLowerDeck,previousUpperDeck;
    public int optionSelected;
    // 1 = Mount
    // Start is called before the first frame update
    void Start()
    {
        currentLowerDeck = null;
        currentUpperDeck = null;
        optionSelected = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
