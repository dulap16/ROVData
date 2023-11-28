using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LimitsManager : MonoBehaviour
{
    public Slider lower;
    public Slider upper;

    public TMP_InputField lowerText;
    public TMP_InputField upperText;

    public CheckColliderBehaviour lowerColl;
    public CheckColliderBehaviour upperColl;

    public Handler h;


    public int _lowerValue;
    public int _upperValue;


    void Start()
    {
        lowerColl = lower.gameObject.GetComponent<CheckColliderBehaviour>();
        upperColl = upper.gameObject.GetComponent<CheckColliderBehaviour>();

        Reset();
    }

    void Update()
    {
        if (lowerColl.clicked == true || upperColl.clicked == true)
        {
            // WON'T CALL METHODS HERE AS IT IS TOO INEFFICIENT

            _lowerValue = (int)(h.max * lower.normalizedValue);
            _upperValue = (int)(h.max * upper.normalizedValue);

            lowerText.text = _lowerValue.ToString();
            upperText.text = _upperValue.ToString();

            h.ValuesClick();
        }
    }

    public void setLowerValue(int val)
    {
        _lowerValue = val;

        equalizeValues();
        h.ValuesClick();
    }

    public void setUpperValue(int val)
    {
        _upperValue = val;

        equalizeValues();
        // h.ValuesClick();
    }


    public void lowerSliderMoved()
    {
        setLowerValue((int)(h.max * lower.normalizedValue));
    }

    public void upperSliderMoved()
    {
        setUpperValue((int)(h.max * upper.normalizedValue));
    }

    public void lowerTextChanged()
    {
        setLowerValue(int.Parse(lowerText.text));
    }

    public void upperTextChanged()
    {
        setUpperValue(int.Parse(upperText.text));
    }

    public void Reset()
    {
        setLowerValue(0);
        setUpperValue(h.max);
    }

    private void equalizeValues()
    {
        lowerText.text = _lowerValue.ToString();
        upperText.text = _upperValue.ToString();

        lower.normalizedValue = (float)_lowerValue / (float)h.max;
        upper.normalizedValue = (float)_upperValue / (float)h.max;
    }
}
