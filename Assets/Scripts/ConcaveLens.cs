using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcaveLens : MonoBehaviour
{
    [SerializeField] Text textConcaveLens;

    [SerializeField] Slider concaveLensSlider;
    [SerializeField] ConvexLensNew convexLensNew;
   public float focalLenght = 2f;

    public void ChangeScreenPosition()
    {
        convexLensNew.isPositionChanged = true;

        float newPos = 0f;

        newPos = concaveLensSlider.value;

        newPos = newPos * 10;

        newPos = 5 - newPos;

        newPos = (Mathf.Round(newPos * 10)) / 10;
        // if ((gameObject.transform.localPosition.x - newPos) < 0.5f)
        // {
        //     newPos = gameObject.transform.localPosition.x - 0.5f;
        //     sliderScreen.value = (5 - newPos) / 10f;
        // }

        gameObject.transform.localPosition = new Vector3(newPos, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        textConcaveLens.text = ((5 - newPos) * 10f).ToString();

    }

    // Start is called before the first frame update

}
