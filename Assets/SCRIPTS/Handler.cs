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

    void Start()
    {
        /*namesLines = System.IO.File.ReadAllLines(@"C:\Users\tudor\OneDrive\Documents\Unity\TESTARE 2\Assets\SCRIPTS\listaLoc.txt");

        for (int i = 0; i < namesLines.Length; i++)
        {
            list = namesLines[i].Split('\t');
            regions[i] = list[1];
            dictionary.Add(regions[i], 0);

            Debug.Log(regions[i]);
        }*/

        dictionary = new Dictionary<string, int>();

        foreach (Transform child in suntProst.transform)
        {
            string n = child.gameObject.name;

            if (n.Contains("."))
            {
                n = n.Substring(0, n.Length - 4);
            }

            n = n.ToLower();
            Debug.Log(n);
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
        Selected(optionInd);
        if (selectedValuesOnly == false)
        {
            optionInd.MakeVisible(230, false);
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
            selectedRegion.MakeVisible(150, true);
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
                    childScript.MakeInvisible(0, false);
                    childScript.ind.HideOutline();
                    childScript.ind.FadeOut();
                }
                else
                {
                    childScript.MakeVisible(childScript.alphaValue, false);
                    childScript.ind.ShowOutline();
                    childScript.ind.FadeIn();
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
            lowerLimit.text = "";
            upperLimit.text = "";
        }
        catch { }

        sm.Reset();
        foreach (Transform child in suntProst.transform)
        {
            OverlappingRegion childScript = child.GetComponent<OverlappingRegion>();
            childScript.MakeVisible(childScript.alphaValue, false);
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
                    selectedRegion.MakeInvisible(0, false);
                else selectedRegion.MakeVisible(selectedRegion.alphaValue, false);
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
