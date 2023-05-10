using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
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
    private Color32 c;

    private Material uniqueMat;
    private Renderer rend;
    private Light2D lightObject;

    public int value;
    public byte alphaValue;
    public byte initialAlpha = 150;
    public byte finalAlpha = 230;
    private float radius;

    /// <summary>
    ///  ANIMATION
    /// </summary>
    public int targetAlpha;
    public int ratio = 10;


    public bool selected = false;
    private string regionNameWithCapitals;
    private string etiqueteText;

    public CursorFollower cf;
     
    /* --BORDER WHEN SELECTED - FAIL
    public GameObject backgroundGroup;
    private GameObject background;
    private float xinitscale;
    private float yinitscale;
    private float xfinalscale;
    private float yfinalscale;
    private float proportionWhenSmall = 0.9f;
    private float speed = 1;
    private float sx;
    private float sy;
    private float targetx;
    private float targety;
    */

    // Start is called before the first frame update
    void Start()
    {
        /// FROM THE REGION SCRIPT

        center = GetComponent<Renderer>().bounds.center;
        ind = ((GameObject)Instantiate(indicator, new Vector3(center.x, center.y, -2), Quaternion.identity, GameObject.Find("Canvas").transform.Find("Map").transform.Find("Points Group"))).GetComponent<Indicator>();
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

        alphaValue = initialAlpha;
        c = colorHandler.CalculateShade(value, 10000, initialAlpha);
        SetColor(c);

        MakeVisible(alphaValue, true);

        etiqueteText = regionNameWithCapitals + " : " + value.ToString();
    }

    public void FixedUpdate()
    {
        if (targetAlpha != (int)(GetComponent<Renderer>().material.color.a * 255))
            ChangeAlpha(ratio);
    }

    public void OnMouseOver()
    {
        if (CheckWithinLimits(value))
            MakeVisible(finalAlpha, false);

        if (cf.shown == false)
            cf.MakeVisible();

        if (cf.GetText() != etiqueteText)
            cf.ChangeText(etiqueteText);
    }

    public void OnMouseExit()
    {
        if (selected == false && CheckWithinLimits(value))
            MakeInvisible(initialAlpha, false);

        cf.MakeInvisible();
    }

    public void OnMouseDown()
    {
        handler.ChangeOption(regionNameWithCapitals);

        if (handler.selectedValuesOnly == false)
            handler.Selected(this);
    }

    private static FieldInfo m_FalloffField = typeof(Light2D).GetField("m_FalloffIntensity", BindingFlags.NonPublic | BindingFlags.Instance);

    // ...

    public void SetFalloff(float falloff)
    {
        m_FalloffField.SetValue(lightObject, falloff);
    }

    public void ChangeAlpha(int ratio)
    {
        Color32 c = GetComponent<Renderer>().material.color;
        int newAlpha = c.a + ratio;

        if (Math.Abs(newAlpha - targetAlpha) < Math.Abs(ratio))
        {
            GetComponent<Renderer>().material.color = new Color32(c.r, c.g, c.b, (byte)(targetAlpha));
            return ;
        }

        GetComponent<Renderer>().material.color = new Color32(c.r, c.g, c.b, (byte)(newAlpha));
    }

    public void SetColor(Color32 c)
    {
        float alpha = GetComponent<Renderer>().material.color.a;
        c.a = (byte)(alpha * 255);
        GetComponent<Renderer>().material.color = c;
    }

    public void MakeVisible(byte alphaValue, bool directly)
    {
        if (!directly)
        {
            ratio = 10;
            targetAlpha = alphaValue;
        } else
        {
            Color32 c = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = new Color32(c.r, c.g, c.b, (byte)(alphaValue));
        }
    }

    public void MakeInvisible(byte alphaValue, bool directly)
    {
        if (!directly)
        {
            ratio = -10;
            targetAlpha = alphaValue;
        }
        else
        {
            Color32 c = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = new Color32(c.r, c.g, c.b, (byte)(alphaValue));
        }
    }

    public void ChangeValue(int newValue)
    {
        value = newValue;
        if (handler.colored == true)
            c = colorHandler.CalculateShade(value, 10000, initialAlpha);
        else c = colorHandler.CalculateGrayscale(value, 10000, initialAlpha);
        SetColor(c);
        ind.ChangeValue(value);
        etiqueteText = regionNameWithCapitals + " : " + value.ToString();
    }

    public void Grayscale(int value)
    {
        c = colorHandler.CalculateGrayscale(value, 10000, initialAlpha);
        SetColor(c);
    }

    public void Colored(int value)
    {
        c = colorHandler.CalculateShade(value, 10000, initialAlpha);
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
