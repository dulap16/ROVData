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

    private Vector3 initialPagePos;
    private Vector3 hiddenPagePos;


    // FOR MASS VALUE INPUT
    private List<string> counties;
    private TMP_InputField massValueInput;
    private bool sheetShown = true;
    private Vector3 initIFPos;
    private Vector3 hiddIFPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPagePos = transform.localPosition;
        hiddenPagePos = new Vector3(500, 0, 0);

        initIFPos = massValueInputgo.transform.localPosition;
        hiddIFPos = new Vector3(500, 0, 0);

        counties = handler.dictionary.Keys.ToList<string>();
        MassValueInsertionClicked();
        massValueInput = massValueInputgo.transform.Find("InputField").GetComponent<TMP_InputField>();
    }

    public void Show()
    {
        transform.localPosition = initialPagePos;
    }

    public void Hide()
    {
        transform.localPosition = hiddenPagePos;
    }

    public void ValuePageButtonClicked()
    {
        presentationPage.Hide();
        Show();

        presentationPage.shown = false;
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
                        correctedTokens[l] = tokens[i];
                        l = l + 1;
                    }
                }

            
                int val = int.Parse(correctedTokens[l - 1]);

                string countyName = tokens[0];
                for (int i = 1; i < l - 1; i++)
                    countyName = countyName + " " + correctedTokens[i];
                countyName = countyName.ToLower();

                /// implement levenshtein distance

                if (handler.dictionary.ContainsKey(countyName))
                    handler.ChangeValueOfRegion(countyName, val);
                else
                {
                    Debug.Log(countyName + " not found");
                    Debug.Log("Searching for closest match");

                    int mini = int.MaxValue;
                    string closeMatch = "";
                    foreach (string name in handler.dictionary.Keys)
                    {
                        int dist = LevenshteinDistance(countyName, name);
                        if (mini > dist)
                        {
                            mini = dist;
                            closeMatch = name;
                        }
                    }

                    handler.ChangeValueOfRegion(closeMatch, val);
                }
            } catch(Exception e)
            {
                Debug.Log(e);
                continue;
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
