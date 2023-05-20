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

    public Handler h;


    void Start()
    {
        lowerLimit.text = "0";
        upperLimit.text = h.max.ToString(); // h.max
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (lowerColl.clicked == true || upperColl.clicked == true)
        {
            lowerValue = (int)(h.max * lower.normalizedValue);
            upperValue = (int)(h.max * upper.normalizedValue);

            lowerLimit.text = lowerValue.ToString();
            upperLimit.text = upperValue.ToString();

            h.ValuesClick();
        }
    }

    public void Reset()
    {
        lower.normalizedValue = 0;
        upper.normalizedValue = 1;
    }

    public void SetLowerValue(int value)
    {
        lower.normalizedValue = (float)value / (float)h.max;
    }

    public void SetUpperValue(int value)
    {
        upper.normalizedValue = (float)value / (float)h.max;
    }
}
