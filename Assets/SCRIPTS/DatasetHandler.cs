using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatasetHandler : MonoBehaviour
{
    // CHANGES THE REGION VALUES BASED ON SOME DATASETS OF LOC-VAL PATTERN
    // WILL HAVE MASS INSERTION VALUES SAVED IN IT IN THE FUTURE

    public Handler h;
    public TMP_Dropdown filePicker;
    private string path;
    public List<string> judete;


    // SAVING DATASETS  
    public TMP_InputField titleField;
    private string savedPath;

    private string pathToDatasets;

    void Start()
    {
        pathToDatasets = Path.Join(Application.persistentDataPath, "Datasets");

        if (!Directory.Exists(pathToDatasets))
            Directory.CreateDirectory(pathToDatasets);
        Debug.Log(pathToDatasets);
        CreateFolders();
        savedPath = Path.Join(pathToDatasets, h.curentJudet);
        filePicker.ClearOptions();


        string[] files = Directory.GetFiles(savedPath);
        
        foreach (string file in files)
        {
            if(file.EndsWith(".txt"))
            {
                string[] tokens = file.Split('\\');
                string fileName = tokens[tokens.Length - 1];
                filePicker.options.Add(new TMP_Dropdown.OptionData { text = fileName.Substring(0, fileName.Length - 4)});
            }
        }

        filePicker.onValueChanged.AddListener(delegate { ChangeValues(); });
    }

    public void JudetChanged()
    {
        savedPath = Path.Join(pathToDatasets, h.curentJudet);
        filePicker.ClearOptions();


        string[] files = Directory.GetFiles(savedPath);

        foreach (string file in files)
        {
            if (file.EndsWith(".txt"))
            {
                string[] tokens = file.Split('\\');
                string fileName = tokens[tokens.Length - 1];
                filePicker.options.Add(new TMP_Dropdown.OptionData { text = fileName.Substring(0, fileName.Length - 4) });
            }
        }
    }

    private void CreateFolders()
    {
        foreach(string judet in judete)
        {
            string currentPath = Path.Join(pathToDatasets, judet);
            if (!Directory.Exists(currentPath))
                Directory.CreateDirectory(currentPath);
        }
    }

    private void ChangeValues()
    {
        int index = filePicker.value;
        // string filePath = path + "\\" + filePicker.options[index].text;
        string filePath = savedPath + "/" + filePicker.options[index].text + ".txt";

        string fileName = filePicker.options[index].text;
        titleField.text = fileName;


        h.ResetClick();
        string[] lines = System.IO.File.ReadAllLines(filePath);

        List<string> counties = new List<string>();
        List<int> values = new List<int>();

        h.max = 0;

        foreach (string line in lines)
        {
            if (!(line[0] >= 'A' && line[0] <= 'Z'))
                continue;

            string[] tokens = line.Split(' ');

            string name = "";
            int value = 0;
            for(int i = 0; i < tokens.Length; i++)
            {
                string firstChar = tokens[i].Substring(0, 1).ToLower();
                if (firstChar[0] >= 'a' && firstChar[0] <= 'z')
                {
                    if (name == "")
                        name = tokens[i];
                    else name = name + " " + tokens[i];
                }
                else if (firstChar[0] >= '1' && firstChar[0] <= '9')
                    value = Int32.Parse(tokens[i]);
            }

            /*
            string name = tokens[0];
            for (int i = 1; i < tokens.Length - 2; i++)
                name = name + " " + tokens[i];
            Debug.Log(name);

            int value = Int32.Parse(tokens[tokens.Length - 1]);
            */

            counties.Add(name);
            values.Add(value);

            h.max = Mathf.Max(h.max, value);
        }

        for(int i = 0; i < counties.Count; i++)
        {
            Debug.Log(counties[i]);
            h.ChangeValueOfRegion(counties[i], values[i]);
        }
        
    }

    public void SaveClicked()
    {
        string fileTitle = titleField.text;
        string fileText = h.ValuesToText();

        string file = fileTitle + ".txt";

        string pathToFile = savedPath + "/" + file;

        if(!System.IO.File.Exists(pathToFile))
            filePicker.options.Add(new TMP_Dropdown.OptionData { text = fileTitle });
        File.WriteAllText(pathToFile, fileText);
    }
}
