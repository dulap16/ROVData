using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OverlappingRegion : MonoBehaviour
{
    public ColorHandler colorHandler;
    public Handler handler;

    public Material initialMat;

    /// <summary>
    /// FROM THE REGION SCRIPT
    /// </summary>

    public Indicator ind;
    public GameObject indicator;
    private Vector3 center;
    private Color c;


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


    // SYMBOLS
    [SerializeField] private List<Symbol> symbols;

    // Start is called before the first frame update
    void Start()
    {
        /// GET NECESSARY OBJCETS
        colorHandler = GameObject.Find("ColorHandler").GetComponent<ColorHandler>();
        handler = GameObject.Find("Handler").GetComponent<Handler>();
        cf = GameObject.Find("Cursor Tag").GetComponent<CursorFollower>();

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
        value = UnityEngine.Random.Range(0, handler.max); ind.value = value;

        handler = GameObject.Find("Handler").GetComponent<Handler>();
        handler.dictionary[name] = value;

        c = colorHandler.CalculateShade(value, handler.max);
        c.a = initialAlpha;
        SetColor(c);

        SetTargetAlpha(initialAlpha);

        etiqueteText = regionNameWithCapitals + " : " + value.ToString();


        /// REMAKE SELECTION SYSTEM
        selectionColor = handler.selectionColor;
        selectionColor.a = finalAlpha;
        lerpTime = handler.lerpTime;

        basicColor = c;
        basicColor.a = initialAlpha;
        overColor = basicColor;
        overColor.a = finalAlpha;

        HideAll();
    }

    public void FixedUpdate()
    {
        if (targetColor != GetComponent<Renderer>().material.color || targetColor.a != GetComponent<Renderer>().material.color.a)
        {
            GetComponent<Renderer>().material.color = Color32.Lerp(GetComponent<Renderer>().material.color, targetColor, lerpTime * Time.deltaTime);
        }
    }

    public void OnMouseEnter()
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

        cf.MakeVisible();
    }

    public void Selected()
    {
        targetColor = selectionColor;
    }

    public void SetColor(Color c)
    {
        targetColor = c;

        basicColor = c;
        basicColor.a = initialAlpha;
        overColor = c;
        overColor.a = finalAlpha;
    }

    public void SetTargetAlpha(float a)
    {
        targetColor.a = a;
    }

    public void SetBasicColor(Color c)
    {
        basicColor = c;
    }

    public Color GetBasicColor()
    {
        return basicColor;
    }

    public void SetOverColor(Color c)
    {
        overColor = c;
    }

    public Color GetOverColor(Color c)
    {
        return overColor;
    }

    public void SetTargetColor(Color c)
    {
        targetColor = c;
    }

    public Color GetTargetColor()
    {
        return targetColor;
    }

    public void ChangeValue(int newValue)
    {
        value = newValue;
        if (handler.colored == true)
            c = colorHandler.CalculateShade(value, handler.max);
        else c = colorHandler.CalculateGrayscale(value, handler.max, GetComponent<Renderer>().material.color.a);
        c.a = GetComponent<Renderer>().material.color.a;
        SetColor(c);
        ind.ChangeValue(value);

        HideAll();
        SelectSymbols();
        ShowSelection();
        etiqueteText = regionNameWithCapitals + " : " + value.ToString();
    }

    // SYMBOLS
    public void AddSymbol(Symbol s)
    {
        symbols.Add(s);
    }

    public void HideAll()
    {
        foreach (Symbol s in symbols)
            s.Hide();
    }

    public void ShowAll()
    {
        foreach (Symbol s in symbols)
            s.Show();
    }

    public void SelectSymbols() // based on value
    {
        int howMany = (int)(((float)value / (float)handler.max) * (float)symbols.Count);

        for(int i = 0; i < symbols.Count; i++)
        {
            Symbol temp = symbols[i];
            int randomIndex = UnityEngine.Random.Range(i, symbols.Count);
            symbols[i] = symbols[randomIndex];
            symbols[randomIndex] = temp;
        }

        for(int i = 0; i < symbols.Count; i++)
        {
            if (i < howMany)
                symbols[i].isInSelection = true;
            else symbols[i].isInSelection = false;
        }
    }

    public void ShowSelection()
    {
        foreach (Symbol s in symbols)
            if (s.isInSelection)
                s.Show();
    }

    public void Grayscale(int value)
    {
        c = colorHandler.CalculateGrayscale(value, handler.max, FigureOutAlpha());
        SetColor(c);
    }

    public void Colored(int value)
    {
        c = colorHandler.CalculateShade(value, handler.max);
        c.a = FigureOutAlpha();
        SetColor(c);
    }

    public float FigureOutAlpha()
    {
        if (CheckWithinLimits(value)) 
        {
            if (selected)
                return finalAlpha;
            return initialAlpha;
        }

        return 0;
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
