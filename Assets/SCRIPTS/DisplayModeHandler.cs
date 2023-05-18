using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;

public class DisplayModeHandler : MonoBehaviour
{
    // CHANGES HOW THE VALUES ARE DISPLAYED

    public TMP_Dropdown modeSelector;

    // COLOR
    public GameObject coloredMap;
    private Vector3 colorInitPos;

    // POINTS   
    public GameObject pointsGroup;
    private Vector3 pointsInitPos;

    // SYMBOLS
    public GameObject symbolsGroup;
    private Vector3 symbolsInitPos;

    private Vector3 hiddenPos;
    public Handler handler;

    // Start is called before the first frame update
    void Start()
    {   modeSelector.onValueChanged.AddListener( delegate { modeChanged(); });

        // COLOR
        colorInitPos = coloredMap.transform.localPosition;

        // POINTS
        pointsInitPos = pointsGroup.transform.localPosition;

        // SYMBOLS
        symbolsInitPos = symbolsGroup.transform.position;
        hiddenPos = new Vector3(200, 200, 0);

        changeVisibilityOfColor(true);
        changeVisibilityOfPoints(false);
        changeVisibilityOfSymbols(false);
    }

    private void modeChanged()
    {
        int index = modeSelector.value;
        string option = modeSelector.options[index].text.ToString();

        if(index == 0) // COLOR
        {
            changeVisibilityOfColor(true);
            changeVisibilityOfPoints(false);
            changeVisibilityOfSymbols(false);
        } else if(index == 1) // SYMBOLS
        {
            changeVisibilityOfColor(false);
            changeVisibilityOfPoints(false);
            changeVisibilityOfSymbols(true);

        } else if(index == 2) // POINT SIZE
        {
            changeVisibilityOfColor(false);
            changeVisibilityOfPoints(true);
            changeVisibilityOfSymbols(false);
        }
    }

    private void changeVisibilityOfColor(bool vis)
    {
        if (vis == true)
            handler.Colored();
        else handler.Grayscale();
    }

    private void changeVisibilityOfPoints(bool vis)
    {
        if (vis == true) {
            pointsGroup.transform.localPosition = pointsInitPos;
        }
        else
            pointsGroup.transform.localPosition = hiddenPos;
        
    }

    private void changeVisibilityOfSymbols(bool vis)
    {
        if (vis == true)
            symbolsGroup.transform.localPosition = symbolsInitPos;
        else
            symbolsGroup.transform.localPosition = hiddenPos;
    }

}
