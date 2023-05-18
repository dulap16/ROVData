using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OverlappingRegion : MonoBehaviour
{
    public LightController lc;
    public ColorHandler colorHandler;
    public Handler handler;

    public Material initialMat;
    public Material lightMat;
    public GameObject light;
    public float intensityValue;
    public float modifiedValue;

    /// <summary>
    /// FROM THE REGION SCRIPT
    /// </summary>

    public Indicator ind;
    public GameObject indicator;
    private Vector3 center;
    private Color c;

    private Light2D lightObject;

    public int value;
    public float initialAlpha = 0.57f;
    public float finalAlpha = 0.9f;


    public bool selected = false;
    private string regionNameWithCapitals;
    private string etiqueteText;

    public CursorFollower cf;

    /// <summary>
    /// REMAKE SELECTION SYSTEM
    /// </summary>
    public float lerpTime;

    [SerializeField] private Color targetColor;
    private Color basicColor;
    private Color overColor;
    public Color selectionColor;

    // Start is called before the first frame update
    void Start()
    {
        /// FROM THE REGION SCRIPT

        center = GetComponent<Renderer>().bounds.center;
        ind = ((GameObject)Instantiate(indicator, new Vector3(center.x, center.y, 0), Quaternion.identity, GameObject.Find("Canvas").transform.Find("Map").transform.Find("Points Group"))).GetComponent<Indicator>();
        if (name.Contains("."))
            name = name.Substring(0, name.Length - 4);
        name = name.ToLower();
        ind.regionsName = name;
        ind.parent = this.gameObject;
        ind.cf = cf;

        regionNameWithCapitals = CapitaliseForPreview(name);

        ind.transform.GetChild(0).GetChild(0).GetComponent<Canvas>().overrideSorting = true;

        /// COLOR AND TRANSPARENCY
        value = UnityEngine.Random.Range(0, 10000); ind.value = value;

        handler = GameObject.Find("Handler").GetComponent<Handler>();
        handler.dictionary[name] = value;

        c = colorHandler.CalculateShade(value, 10000);
        c.a = initialAlpha;
        SetColor(c);

        MakeVisible(initialAlpha);

        etiqueteText = regionNameWithCapitals + " : " + value.ToString();


        /// REMAKE SELECTION SYSTEM
        selectionColor = handler.selectionColor;
        selectionColor.a = finalAlpha;
        lerpTime = handler.lerpTime;

        basicColor = c;
        basicColor.a = initialAlpha;
        overColor = basicColor;
        overColor.a = finalAlpha;
    }

    public void FixedUpdate()
    {
        if (targetColor != GetComponent<Renderer>().material.color || targetColor.a != GetComponent<Renderer>().material.color.a)
        {
            GetComponent<Renderer>().material.color = Color32.Lerp(GetComponent<Renderer>().material.color, targetColor, lerpTime * Time.deltaTime);
        }
    }

    public void OnMouseOver()
    {
        if (CheckWithinLimits(value))
        {
            targetColor = overColor;
        }

        if (cf.shown == false)
            cf.MakeVisible();

        if (cf.GetText() != etiqueteText)
            cf.ChangeText(etiqueteText);
    }

    public void OnMouseExit()
    {
        if (selected == false && CheckWithinLimits(value))
        {
            targetColor = basicColor;
        }

        cf.MakeInvisible();
    }

    public void OnMouseDown()
    {
        handler.ChangeOption(regionNameWithCapitals);

        if (handler.selectedValuesOnly == false)
            handler.Selected(this);
    }

    public void Selected()
    {
        targetColor = selectionColor;
    }

    private static FieldInfo m_FalloffField = typeof(Light2D).GetField("m_FalloffIntensity", BindingFlags.NonPublic | BindingFlags.Instance);

    // ...

    public void SetFalloff(float falloff)
    {
        m_FalloffField.SetValue(lightObject, falloff);
    }

    public void SetColor(Color c)
    {
        targetColor = c;

        basicColor = c;
        basicColor.a = initialAlpha;
        overColor = c;
        overColor.a = finalAlpha;
    }

    public void MakeVisible(float a)
    {
        targetColor.a = a;
    }

    public void ChangeValue(int newValue)
    {
        value = newValue;
        if (handler.colored == true)
            c = colorHandler.CalculateShade(value, 10000);
        else c = colorHandler.CalculateGrayscale(value, 10000, GetComponent<Renderer>().material.color.a);
        c.a = GetComponent<Renderer>().material.color.a;
        SetColor(c);
        ind.ChangeValue(value);
        etiqueteText = regionNameWithCapitals + " : " + value.ToString();
    }

    public void Grayscale(int value)
    {
        c = colorHandler.CalculateGrayscale(value, 10000, GetComponent<Renderer>().material.color.a);
        SetColor(c);
    }

    public void Colored(int value)
    {
        c = colorHandler.CalculateShade(value, 10000);
        c.a = GetComponent<Renderer>().material.color.a;
        SetColor(c);
    }

    public bool CheckWithinLimits(int value)
    {
        return handler.selectedValuesOnly == false || (handler.selectedValuesOnly == true && (value >= handler.ll && value <= handler.ul)); 
    }

    public string CapitaliseForPreview(string name)
    {
        char[] newName = new char[name.Length];

        for (int i = 0; i < name.Length; i++)
        {
            if (i == 0 || (i > 0 && name[i - 1] == ' '))
            {
                newName[i] = (char)((int)name[i] + 'A' - 'a');
            }
            else newName[i] = name[i];
        }

        return new string(newName);
    }

}
