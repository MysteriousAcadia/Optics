using UnityEngine;
using UnityEngine.UI;

public class UISliderHandler : MonoBehaviour
{
    [SerializeField] float minValue, maxValue;
    [SerializeField] string unit;
    [SerializeField] string valueTop;
    [SerializeField] int decimalCount;
    [SerializeField] Text valueTopText;
    [SerializeField] Text valueText;
    [SerializeField] Text unitText,minValueText, maxValueText;

    private void Start()
    {
        unitText.text = unit;
        minValueText.text = minValue.ToString() + " " + unit;
        maxValueText.text = maxValue.ToString() + " " + unit;
        valueTopText.text = valueTop;
    }

    public void UpdateTextBox(float value)
    {
        float rounder = Mathf.Pow(10f,decimalCount);
        valueText.text = (Mathf.Round((value * (maxValue - minValue) + minValue)*rounder)/rounder).ToString();
    }
}
