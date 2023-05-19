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

    // Start is called before the first frame update
    void Start()
    {
        english = new string[] {"Value Settings", "Presentation Settings", "Change", "Mass Value Insertion", "Insert Values", 
                                "Values", "Reset", "Presentation Mode", "Changed Value", "This is for inserting an entiere new data set for the map '\n\n' " +
                                "Use it by pasting here the set in the format LOC - VAL", "Lower Limit", "Upper Limit", "Show Cadastre", "Title", "Cadastre Opacity", "Save", "Dataset name..."};

        romana = new string[] { "Valori", "Prezentare", "Schimbă", "Inserție de valori în masă", "Inserează Valori", "Arată Valori", "Resetează", "Modul de prezentare", 
                                "Valoarea Schimbată", "Acest câmp este pentru inserarea unui set de date complet nou pentru hartă \n\n " +
                                "Folosiți-l prin a lipi aici setul de date în formatul LOC - VAL", "Limita de jos", "Limita de sus", "Arata Cadastrul", "Titlu", "Opacitatea Cadastrului", "Salvează", "Numele setului..."};
        n = tmptexts.Length;

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
