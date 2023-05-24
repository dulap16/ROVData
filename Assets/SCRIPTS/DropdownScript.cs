using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour
{
    // CONTROLS DROPDOWN THAT LETS YOU CHANGE THE VALUE OF A REGION

    public Handler handler;
    public TMP_InputField IF;

    private TMP_Dropdown dd;
    // Start is called before the first frame update
    void Start()
    {
        dd = GetComponent<TMP_Dropdown>();
        dd.ClearOptions();

        foreach(string key in handler.dictionary.Keys)
        {
            dd.options.Add(new TMP_Dropdown.OptionData() { text = key });
        }

        dd.onValueChanged.AddListener(delegate { DropdownItemSelected(dd); });

        Debug.Log(this.name);
    }

    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        IF.text = handler.dictionary[dropdown.options[index].text].ToString();
        Debug.Log(dropdown.options[index].text);
    }

    public void JudetChanged()
    {
        dd.ClearOptions();
        foreach (string key in handler.dictionary.Keys)
        {
            dd.options.Add(new TMP_Dropdown.OptionData() { text = key });
        }
    }
}
