using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject currentLowerDeck,upperLowerDeck;
    public int optionSelected;
    // Start is called before the first frame update
    void Start()
    {
        currentActiveCanvas = null;
        optionSelected = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
