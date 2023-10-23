using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ValuePageHandler : MonoBehaviour
{
    // CONTROLS THE CHANGING OF VALUES


    // FOR WHEN CHANGING TO PRESENTATION
    public Handler handler;
    public PresentationPageHandler presentationPage;
    public GameObject massValueInputgo;
    public GameObject insertValuesButton;


    // FOR MASS VALUE INPUT
    private TMP_InputField massValueInput;
    private bool sheetShown = true;
    private Vector3 initIFPos;
    private Vector3 hiddIFPos;

    // Start is called before the first frame update
    void Start()
    {

        initIFPos = massValueInputgo.transform.localPosition;
        hiddIFPos = new Vector3(500, 0, 0);

        MassValueInsertionClicked();
        massValueInput = massValueInputgo.transform.Find("InputField").GetComponent<TMP_InputField>();
    }

    public void MassValueInsertionClicked()
    {
        if (sheetShown)
        {
            sheetShown = false;
            massValueInputgo.transform.localPosition = hiddIFPos;
        }
        else
        {
            sheetShown = true;
            massValueInputgo.transform.localPosition = initIFPos;
        }
    }

    public void InsertValuesClicked()
    {
        string input = massValueInput.text;
        string[] lines = input.Split('\n');
        handler.max = 0;

        List<string> counties = new List<string>();
        List<int> values = new List<int>();

        foreach (string line in lines)
        {
            try
            {
                string[] tokens = line.Split(' ');
                string[] correctedTokens = new string[10];
                int l = 0;
                for (int i = 0; i < tokens.Length; i++)
                {
                    if ((tokens[i][0] >= '0' && tokens[i][0] <= '9') || (tokens[i][0] >= 'A' && tokens[i][0] <= 'z'))
                    {
                        correctedTokens[l] = tokens[i].ToLower();
                        l = l + 1;
                    }
                }

                string name = "";
                int val = 0;

                for(int i = 0; i < l; i++)
                {
                    string firstChar = correctedTokens[i].Substring(0, 1);
                    if (firstChar[0] >= 'a' && firstChar[0] <= 'z')
                    {
                        if (name == "")
                            name = correctedTokens[i];
                        else name = name + " " + correctedTokens[i];
                    }
                    else if (firstChar[0] >= '1' && firstChar[0] <= '9')
                        val = Int32.Parse(correctedTokens[i]);
                }

                counties.Add(name);
                values.Add(val);

                handler.max = Mathf.Max(handler.max, val);
            } catch(Exception e)
            {
                Debug.Log(e);
                Debug.Log(line);
                continue;
            }
        }

        for(int i = 0; i < counties.Count; i++)
        {
            /// implement levenshtein distance

            string name = counties[i];
            int val = values[i];

            if (handler.dictionary.ContainsKey(name))
                handler.ChangeValueOfRegion(name, val);
            else
            {
                Debug.Log(name + " not found");
                Debug.Log("Searching for closest match");

                int mini = int.MaxValue;
                string closeMatch = "";
                foreach (string county in handler.dictionary.Keys)
                {
                    int dist = LevenshteinDistance(name, county);
                    if (mini > dist)
                    {
                        mini = dist;
                        closeMatch = county;
                    }
                }

                handler.ChangeValueOfRegion(closeMatch, val);
            }
        }
    }

    public int LevenshteinDistance(string a, string b)
    {
        int[][] dp = new int[30][];

        int l1 = a.Length;
        int l2 = b.Length;

        a = a.ToUpper();
        b = b.ToUpper();

        Debug.Log(a);
        Debug.Log(b);

        for (int i = 0; i <= l1; i++)
        {
            dp[i] = new int[30];
            dp[i][0] = i;

        }
        for (int i = 0; i <= l2; i++)
        {
            dp[0][i] = i;
        }

        for(int i = 1; i <= l1; i++)
        {
            for(int j = 1; j <= l2; j++)
            {
                dp[i][j] = Mathf.Min(dp[i - 1][j], dp[i][j - 1], dp[i - 1][j - 1]);

                if (a[i - 1] != b[j - 1])
                    dp[i][j]++;
            }
        }

        return dp[l1][l2];
    }
}
