using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    // CONTROLS THE SLIDERS THE HELP WITH SELECTING THE LIMITS

    public Slider lower;
    public Slider upper;

    public CheckColliderBehaviour lowerColl;
    public CheckColliderBehaviour upperColl;

    public int lowerValue;
    public int upperValue;

    public TMP_InputField lowerLimit;
    public TMP_InputField upperLimit;

    public int minValue;
    public int maxValue = 10000;

    public Handler h;


    // Update is called once per frame
    void Update()
    {
        if (lowerColl.clicked == true || upperColl.clicked == true)
        {
            lowerValue = (int)(maxValue * lower.normalizedValue);
            upperValue = (int)(maxValue * upper.normalizedValue);

            lowerLimit.text = lowerValue.ToString();
            upperLimit.text = upperValue.ToString();

            h.ValuesClick();
        }
    }

    public void Reset()
    {
        lower.normalizedValue = 0;
        upper.normalizedValue = 0;
    }

    public void SetLowerValue(int value)
    {
        lower.normalizedValue = (float)value / (float)maxValue;
    }

    public void SetUpperValue(int value)
    {
        upper.normalizedValue = (float)value / (float)maxValue;
    }
}
