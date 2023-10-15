using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Handler : MonoBehaviour
{
    // HANDLES MOST ELEMENTS OF THE APP

    public NewUIHandler newUIHandler;


    // SELECTION
    public bool selectedValuesOnly = false;
    public OverlappingRegion current;

    // REGION DROPDOWN
    public TMP_InputField IF;
    public TMP_Dropdown dd;

    // MAP CONTROL
    public GameObject blender;
    public GameObject judetGO;
    public GameObject cadastru;
    public string[] regions;
    public Dictionary<string, int> dictionary;

    // LIMIT INPUT FIELDS AND BUTTONS
    public TMP_InputField lowerLimit;
    public TMP_InputField upperLimit;
    public Button showLimits;
    public Button resetLimits;
    public int mode = 0;

    public int ratio = 10;

    // LIMIT SLIDERS
    public int ll;
    public int ul;
    public LimitsManager limitsManager;

    public bool colored = true;
    public Color selectionColor;
    public float lerpTime = 4;

    // SYMBOLS
    public GameObject symbolPrefab;

    public GameObject indprefab;

    // MAXIMUM
    public int max = 10000;


    public string curentJudet = "Alba";
    public PresentationPageHandler pphandler;

    public void Start()
    {
        foreach(Transform child in blender.transform)
        {
            if (child.name == "CADASTRU")
                cadastru = child.gameObject;
            else judetGO = child.gameObject;
        }

        /*pphandler.cadastre = cadastru;*/

        dictionary = new Dictionary<string, int>();

        foreach (Transform child in judetGO.transform)
        {
            string n = child.gameObject.name;

            if (n.Contains("."))
            {
                n = n.Substring(0, n.Length - 4);
            }

            n = n.ToLower();
            dictionary.Add(n, 0);
        }

        dd.ClearOptions();

        foreach (string key in dictionary.Keys)
        {
            // Debug.Log(key);
            dd.options.Add(new TMP_Dropdown.OptionData() { text = CapitaliseForPreview(key) });
        }

        dd.onValueChanged.AddListener(delegate { DropdownItemSelected(); });

        // IF.onValueChanged.AddListener(delegate { ChangeValueOfRegion(IF); });

        current = null;
        colored = true;
    }

    public void JudetChanged()
    {
        ResetClick();

        // REPEAT HANDLER START()
        foreach (Transform child in blender.transform)
        {
            if (child.name == "CADASTRU")
                cadastru = child.gameObject;
            else judetGO = child.gameObject;
        }

        dictionary.Clear();

        foreach (Transform child in judetGO.transform)
        {
            string n = child.gameObject.name;

            if (n.Contains("."))
            {
                n = n.Substring(0, n.Length - 4);
            }

            n = n.ToLower();
            // Debug.Log(n);
            dictionary.Add(n, 0);
        }

        dd.ClearOptions();

        foreach (string key in dictionary.Keys)
        {
            dd.options.Add(new TMP_Dropdown.OptionData() { text = CapitaliseForPreview(key) });
        }
    }

    void DropdownItemSelected()
    {
        int index = dd.value;

        IF.text = dictionary[dd.options[index].text.ToLower()].ToString();

        OverlappingRegion optionInd = GameObject.Find(dd.options[index].text.ToString().ToLower()).GetComponent<OverlappingRegion>();
        
        // COPIED FROM Selected() METHOD
        if (current)
        {
            current.selected = false;
            current.ind.selected = false;
            current.ind.OnMouseExit();
            current.OnMouseExit();
        }

        current = optionInd;
        current.selected = true;
        current.ind.selected = true;
        newUIHandler.SelectedRegionChanged();
        // --------------

        if (optionInd.CheckWithinLimits(optionInd.value))
        {
            optionInd.SetTargetAlpha(optionInd.finalAlpha);
        }
        // optionInd.tag.MakeInvisible();

        Debug.Log(dd.options[index].text);
    }

    public void ChangeValueOfRegion(string regionName, int value)
    {
        try
        {
            regionName = regionName.ToLower();
            OverlappingRegion selectedRegion = judetGO.transform.Find(regionName).GetComponent<OverlappingRegion>();

            dictionary[regionName] = value;
            Debug.Log(max);
            selectedRegion.ChangeValue(value);
            selectedRegion.SetTargetAlpha(selectedRegion.initialAlpha);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log(regionName);
        }
    }

    public void ChangeOption(string regionName)
    {
        int index = dd.options.FindIndex((i) => { return i.text.Equals(regionName); });
        Debug.Log(index);

        dd.value = index;
    }

    public void Selected(OverlappingRegion OR)
    {
        if (current)
        {
            current.selected = false;
            current.ind.selected = false;
            current.ind.OnMouseExit();
            current.OnMouseExit();
        }

        current = OR;
        current.selected = true;
        current.OnMouseEnter();
        current.ind.selected = true;
        current.ind.OnMouseOver();
    }

    public void ValuesClick()
    {
        Debug.Log("changed");

        try
        {
            selectedValuesOnly = true;

            ll = Int32.Parse(lowerLimit.text);
            ul = Int32.Parse(upperLimit.text);

            /*sm.SetLowerValue(ll);
            sm.SetUpperValue(ul);*/

            foreach (Transform child in judetGO.transform)
            {
                OverlappingRegion childScript = child.GetComponent<OverlappingRegion>();
                int nr = childScript.value;
                Debug.Log(nr);
                if (nr < ll || nr > ul)
                {
                    childScript.SetTargetAlpha(0);
                    childScript.ind.HideOutline();
                    childScript.ind.FadeOut();
                }
                else
                {
                    if (current != childScript)
                    {
                        childScript.SetTargetAlpha(childScript.initialAlpha);
                        childScript.ind.ShowOutline();
                        childScript.ind.FadeIn();
                    } else
                    {
                        childScript.OnMouseEnter();
                        childScript.OnMouseExit();
                    }

                    childScript.SetBasicColor(childScript.GetTargetColor());
                }

            }
        }
        catch { }
    }


    public bool isReset()
    {
        if (limitsManager._lowerValue == 0 && limitsManager._upperValue == max)
            return true;
        else return false;
    }

    public void ChangeClick()
    {
        try
        {
            int newValue = Int32.Parse(IF.text);

            string region = dd.options[dd.value].text.ToLower();
            dictionary[region] = newValue;

            OverlappingRegion selectedRegion = judetGO.transform.Find(region).GetComponent<OverlappingRegion>();
            selectedRegion.ChangeValue(newValue);


            if (selectedValuesOnly == true)
            {
                if (newValue < ll || newValue > ul)
                    selectedRegion.SetTargetAlpha(0);
                else selectedRegion.SetTargetAlpha(selectedRegion.initialAlpha);
            }
        }
        catch (Exception e) { Debug.Log(e); }
    }

    public void Grayscale()
    {
        foreach(Transform child in judetGO.transform)
        {
            OverlappingRegion region = child.GetComponent<OverlappingRegion>();
            region.Grayscale(region.value);
        }

        colored = false;
    }

    public void Colored()
    {
        foreach (Transform child in judetGO.transform)
        {
            OverlappingRegion region = child.GetComponent<OverlappingRegion>();
            region.Colored(region.value);
        }

        colored = true;
    }

    public void Transparent()
    {
        foreach(Transform child in judetGO.transform)
        {
            OverlappingRegion region = child.GetComponent<OverlappingRegion>();

            Color c = region.GetBasicColor();
            c.a = 0;
            region.SetBasicColor(c);
            region.SetTargetAlpha(0);
        }
    }    

    public void DelegateSymbols()
    {
        foreach (Transform child in judetGO.transform)
        {
            Debug.Log(child.name);
            OverlappingRegion region = child.GetComponent<OverlappingRegion>();
            region.HideAll();
            region.SelectSymbols();
            region.ShowSelection();
        }
    }

    public List<float> CalculateBoundsOfGroup()
    {
        float xmin = 1000, ymin = 1000, xmax = -1000, ymax = -1000;

        foreach(Transform child in judetGO.transform)
        {
            Renderer mesh = child.GetComponent<Renderer>();

            xmin = Mathf.Min(xmin, mesh.bounds.center.x - mesh.bounds.extents.x);
            xmax = Mathf.Max(xmax, mesh.bounds.center.x + mesh.bounds.extents.x);
            ymin = Mathf.Min(ymin, mesh.bounds.center.y - mesh.bounds.extents.y);
            ymax = Mathf.Max(ymax, mesh.bounds.center.y + mesh.bounds.extents.y);
        }

        List<float> l = new List<float> { xmin, xmax, ymin, ymax };
        return l;
    }

    public string ValuesToText()
    {
        string text = "";
        foreach (Transform child in judetGO.transform)
        {
            OverlappingRegion region = child.GetComponent<OverlappingRegion>();
            text = text + CapitaliseForPreview(region.name) + " " + region.value + '\n';
        }

        return text;
    }


    public void AssignRandomValues()
    {
        foreach (string key in dictionary.Keys.ToList<string>())
        {
            int randvalue = UnityEngine.Random.Range(1, 10000);
            ChangeValueOfRegion(key, randvalue);
        }
    }

    // COPIED FROM INDICATOR SCRIPT
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


    public bool nrFormattingOn = false;
    public void ToggleNrFormatting()
    {
        if (nrFormattingOn == false)
        {
            nrFormattingOn = true;

            foreach(Transform child in judetGO.transform)
            {
                OverlappingRegion region = child.GetComponent<OverlappingRegion>();
                region.ToggleNrFormatting();
            }
        } else
        {
            nrFormattingOn = false;

            foreach (Transform child in judetGO.transform)
            {
                OverlappingRegion region = child.GetComponent<OverlappingRegion>();
                region.ToggleNrFormatting();
            }
        }
    }

    public string NrFormatter(int nr)
    {
        if (nr < 1000)
            return nr.ToString();

        if(nr < 1000000)
        {
            float thousand = (float)nr / 1000f;
            thousand = Mathf.Round(thousand * 100f) / 100f;
            return thousand + "K";
        }

        float million = (float)nr / 1000000f;
        million = Mathf.Round(million * 100f) / 100f;
        return million + "M";
    }
}
