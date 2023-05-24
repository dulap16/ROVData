using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

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
    public SortingGroup symbolsGroup;
    private int initLayer;
    private int hiddenLayer = -1;

    private Vector3 hiddenPos;
    public Handler handler;

    // Start is called before the first frame update
    void Start()
    {   modeSelector.onValueChanged.AddListener( delegate { modeChanged(); });
        coloredMap = handler.judet;

        // COLOR
        colorInitPos = coloredMap.transform.localPosition;

        // POINTS
        pointsInitPos = pointsGroup.transform.localPosition;

        // SYMBOLS
        initLayer = symbolsGroup.sortingOrder;
        hiddenPos = new Vector3(200, 200, 0);

        modeChanged(0);
    }

    public void modeChanged()
    {
        int index = modeSelector.value;
        string option = modeSelector.options[index].text.ToString();

        if(index == 0) // COLOR
        {
            changeVisibilityOfColor(true);
        } else if(index == 1) // SYMBOLS
        {
            changeVisibilityOfPoints(true);
        } else if(index == 2) // POINT SIZE
        {
            changeVisibilityOfSymbols(true);
        }
    }

    public void modeChanged(int index)
    {
        if (index == 0) // COLOR
        {
            changeVisibilityOfColor(true);
            changeVisibilityOfPoints(false);
            changeVisibilityOfSymbols(false);
        }
        else if (index == 1) // SYMBOLS
        {
            changeVisibilityOfColor(false);
            changeVisibilityOfPoints(true);
            changeVisibilityOfSymbols(false);
        }
        else if (index == 2) // POINT SIZE
        {
            changeVisibilityOfColor(false);
            changeVisibilityOfPoints(false);
            changeVisibilityOfSymbols(true);
        }

        modeSelector.value = index;
    }

    private void changeVisibilityOfColor(bool vis)
    {
        /*if (vis == true)
            coloredMap.transform.localPosition = colorInitPos;
        else coloredMap.transform.localPosition = hiddenPos;*/
        if (vis == true)
        {
            handler.mode = 0;

            handler.Colored();
            changeVisibilityOfPoints(false);
            changeVisibilityOfSymbols(false);
        }
        else
        {
            handler.Grayscale();

            if(handler.isReset())
                handler.Transparent();
        }
    }

    private void changeVisibilityOfPoints(bool vis)
    {
        if (vis == true) {
            handler.mode = 1;

            pointsGroup.transform.localPosition = pointsInitPos;
            changeVisibilityOfColor(false);
            changeVisibilityOfSymbols(false);
        }
        else
            pointsGroup.transform.localPosition = hiddenPos;
        
    }

    private void changeVisibilityOfSymbols(bool vis)
    {
        if (vis == true)
        {
            handler.mode = 2;

            symbolsGroup.sortingOrder = initLayer;
            changeVisibilityOfColor(false);
            changeVisibilityOfPoints(false);
        }
        else
            symbolsGroup.sortingOrder = hiddenLayer;
    }

    public void JudetChanged()
    {
        // reset pointsGroup
        foreach(Transform point in pointsGroup.transform)
        {
            Destroy(point.gameObject);
        }

        // reset symbolsGroup
        foreach(Transform symbol in symbolsGroup.transform)
        {
            Destroy(symbol.gameObject);
        }

        modeChanged(0);
    }
}
