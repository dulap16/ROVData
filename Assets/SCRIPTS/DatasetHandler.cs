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


    // SAVING DATASETS  
    public TMP_InputField titleField;
    private string savedPath = "C:\\Users\\tudor\\OneDrive\\Documents\\Unity\\Proiect Galati\\Assets\\Saved Datasets";

    void Start()
    {
        path = Application.dataPath + "/DataSets/Romania/Galati";
        savedPath = Application.dataPath + "/Saved Datasets";
        filePicker.ClearOptions();


        string[] files = Directory.GetFiles(savedPath);

        filePicker.options.Add(new TMP_Dropdown.OptionData { text = "Random1" });
        filePicker.options.Add(new TMP_Dropdown.OptionData { text = "Random2" });
        
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

    private void ChangeValues()
    {
        int index = filePicker.value;
        // string filePath = path + "\\" + filePicker.options[index].text;
        string filePath = savedPath + "/" + filePicker.options[index].text + ".txt";

        if (filePicker.options[index].text == "Random1" || filePicker.options[index].text == "Random2")
        {
            h.AssignRandomValues();
        }
        else 
        {
            h.ResetClick();
            string[] lines = System.IO.File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (!(line[0] >= 'A' && line[0] <= 'Z'))
                    continue;

                string[] tokens = line.Split(' ');
                Debug.Log(tokens.Length);
                string name = tokens[0];
                for (int i = 1; i < tokens.Length - 2; i++)
                    name = name + " " + tokens[i];

                int value = Int32.Parse(tokens[tokens.Length - 1]);

                h.ChangeValueOfRegion(name, value);
            }
        }
    }

    public void SaveClicked()
    {
        string fileTitle = titleField.text;
        string fileText = h.ValuesToText();

        string file = fileTitle + ".txt";

        string pathToFile = savedPath + "/" + file;
        File.WriteAllText(pathToFile, fileText);

        filePicker.options.Add(new TMP_Dropdown.OptionData { text = fileTitle }); 
    }
}
