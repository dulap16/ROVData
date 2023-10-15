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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
