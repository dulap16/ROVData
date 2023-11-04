using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    private string[] english;
    private string[] romana;
    private List<string> ddenglish;
    private List<string> ddromana;

    public TMP_Text[] tmptexts;
    public TMP_Dropdown displayMode;

    private int n;

    
    [Serializable]
    public class Translation
    {
        public TMP_Text obj;

        public string romanian;
        public string english;

        public void toRomanian()
        {
            obj.text = romanian;
        }

        public void toEnglish()
        {
            obj.text = english;
        }
    }

    [Serializable]
    public class TranslationList
    {
        public List<Translation> list;

        public void toRomanian()
        {
            foreach(Translation t in list)
            {
                t.toRomanian();
            }
        }

        public void toEnglish()
        {
            foreach(Translation t in list)
            {
                t.toEnglish();
            }
        }
    }

    [SerializeField] public TranslationList tl;

    // Start is called before the first frame update
    void Start()
    {
        n = tmptexts.Length;

        english = new string[] {"Change", "Mass Value Insertion", "Insert Values", 
                                "Reset", "Presentation Mode", "Changed Value", "This field is for inserting an entiere new data set for the map '\n\n' " +
                                "Use it by pasting here the set in the format LOC - VAL", "Lower Limit", "Upper Limit", "Title", "Cadastre Opacity", "Save", "Dataset name...",
                                "Randomise", "Format Values"};

        romana = new string[] { "Schimbă", "Inserție de valori în masă", "Inserează Valori", "Resetează", "Modul de prezentare", 
                                "Valoarea Schimbată", "Acest câmp este pentru inserarea unui set de date complet nou pentru hartă \n\n " +
                                "Folosiți-l prin a lipi aici setul de date în formatul LOC - VAL", "Limita de jos", "Limita de sus", "Titlu", "Opacitatea Cadastrului", "Salvează", "Numele setului...",
                                "Aleatoriu", "Formateaza valorile"};

        ddenglish = new List<string> { "Color", "Point Size", "Symbols" };
        ddromana = new List<string> { "Culoare", "Puncte", "Simboluri" };

        ChangeToRomanian();
    }

    public void ChangeToRomanian()
    {
        for(int i = 0; i < n; i++)
        {
            tmptexts[i].text = romana[i];
        }

        displayMode.ClearOptions();
        displayMode.AddOptions(ddromana);
    }

    public void ChangeToEnglish()
    {
        for (int i = 0; i < n; i++)
        {
            tmptexts[i].text = english[i];
        }

        displayMode.ClearOptions();
        displayMode.AddOptions(ddenglish);
    }
}
