using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConvexMirror: MonoBehaviour
{
    public float focalLenght = 1.5f;
    [SerializeField] Text textConvexMirror;
    [SerializeField] Slider convexMirrorSlider;
    [SerializeField] ConvexLensNew convexLensNew;

    // Start is called before the first frame update

    public void ChangeScreenPosition()
    {
        convexLensNew.isPositionChanged = true;

        float newPos = 0f;

        newPos = convexMirrorSlider.value;

        newPos = newPos * 10;

        newPos = 5 - newPos;

        newPos = (Mathf.Round(newPos * 10)) / 10;
        // if ((gameObject.transform.localPosition.x - newPos) < 0.5f)
        // {
        //     newPos = gameObject.transform.localPosition.x - 0.5f;
        //     sliderScreen.value = (5 - newPos) / 10f;
        // }

        gameObject.transform.localPosition = new Vector3(newPos, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        textConvexMirror.text = ((5 - newPos) * 10f).ToString();

    }

    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
