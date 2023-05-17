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


    // SELECTION
    public bool selectedValuesOnly = false;
    public OverlappingRegion current;

    // REGION DROPDOWN
    public TMP_InputField IF;
    public TMP_Dropdown dd;

    // MAP CONTROL
    public GameObject suntProst;
    public string[] regions;
    public Dictionary<string, int> dictionary;

    // LIMIT INPUT FIELDS AND BUTTONS
    public TMP_InputField lowerLimit;
    public TMP_InputField upperLimit;
    public Button showLimits;
    public Button resetLimits;

    public int ratio = 10;

    // LIMIT SLIDERS
    public int ll;
    public int ul;
    public SliderManager sm;

    public bool colored = true;
    public Color selectionColor;
    public float lerpTime = 4;

    void Start()
    {
        dictionary = new Dictionary<string, int>();

        foreach (Transform child in suntProst.transform)
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

        dd.onValueChanged.AddListener(delegate { DropdownItemSelected(); });

        // IF.onValueChanged.AddListener(delegate { ChangeValueOfRegion(IF); });

        current = null;
        colored = true;
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
        // --------------

        if (optionInd.CheckWithinLimits(optionInd.value))
        {
            optionInd.MakeVisible(optionInd.finalAlpha);
        }
        // optionInd.tag.MakeInvisible();

        Debug.Log(dd.options[index].text);
    }

    public void ChangeValueOfRegion(string regionName, int value)
    {
        try
        {
            regionName = regionName.ToLower();
            OverlappingRegion selectedRegion = suntProst.transform.Find(regionName).GetComponent<OverlappingRegion>();

            dictionary[regionName] = value;
            selectedRegion.ChangeValue(value);
            selectedRegion.MakeVisible(selectedRegion.initialAlpha);
        }
        catch (Exception e)
        {
            Debug.Log(e);
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
        current.ind.selected = true;
        current.ind.OnMouseOver();
    }

    public void ValuesClick()
    {
        try
        {
            selectedValuesOnly = true;

            ll = Int32.Parse(lowerLimit.text);
            ul = Int32.Parse(upperLimit.text);

            sm.SetLowerValue(ll);
            sm.SetUpperValue(ul);

            foreach (Transform child in suntProst.transform)
            {
                OverlappingRegion childScript = child.GetComponent<OverlappingRegion>();
                int nr = childScript.value;
                Debug.Log(nr);
                if (nr < ll || nr > ul)
                {
                    childScript.MakeVisible(0);
                    childScript.ind.HideOutline();
                    childScript.ind.FadeOut();
                }
                else
                {
                    if (current != childScript)
                    {
                        childScript.MakeVisible(childScript.initialAlpha);
                        childScript.ind.ShowOutline();
                        childScript.ind.FadeIn();
                    } else
                    {
                        childScript.OnMouseOver();
                        childScript.OnMouseExit();
                    }
                }

            }
        }
        catch { }
    }

    public void ResetClick()
    {
        selectedValuesOnly = false;

        try
        {
            lowerLimit.text = "0";
            upperLimit.text = "10000";           // MAX
        }
        catch { }

        sm.Reset();
        foreach (Transform child in suntProst.transform)
        {
            OverlappingRegion childScript = child.GetComponent<OverlappingRegion>();
            childScript.MakeVisible(childScript.initialAlpha);
            childScript.ind.HideOutline();
            childScript.ind.FadeIn();
        }
    }

    public void ChangeClick()
    {
        try
        {
            int newValue = Int32.Parse(IF.text);

            string region = dd.options[dd.value].text.ToLower();
            dictionary[region] = newValue;

            OverlappingRegion selectedRegion = suntProst.transform.Find(region).GetComponent<OverlappingRegion>();
            selectedRegion.ChangeValue(newValue);


            if (selectedValuesOnly == true)
            {
                if (newValue < ll || newValue > ul)
                    selectedRegion.MakeVisible(0);
                else selectedRegion.MakeVisible(selectedRegion.initialAlpha);
            }
        }
        catch (Exception e) { Debug.Log(e); }
    }

    public void Grayscale()
    {
        foreach(Transform child in suntProst.transform)
        {
            OverlappingRegion region = child.GetComponent<OverlappingRegion>();
            region.Grayscale(region.value);
        }

        colored = false;
    }

    public void Colored()
    {
        foreach (Transform child in suntProst.transform)
        {
            OverlappingRegion region = child.GetComponent<OverlappingRegion>();
            region.Colored(region.value);
        }

        colored = true;
    }

    public string ValuesToText()
    {
        string text = "";
        foreach (Transform child in suntProst.transform)
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
}
