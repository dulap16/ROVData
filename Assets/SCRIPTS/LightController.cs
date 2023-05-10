using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Color32 color = Color.red;

    public float imultiplier = 1;
    public float ifixedAddition = 0.5f;

    public float rmultiplier = 1600;
    public float rfixedAddition = 0;

    public float falloffIntensity = 0.5f;

    public bool power = true;

    private Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
    }
}
