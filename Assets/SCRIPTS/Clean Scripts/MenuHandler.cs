using Assets.SCRIPTS.Start_Page;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public ObjectToBeLerped shade;
    private ObjectToBeLerped menuGo;
    private ObjectToBeLerped sidePanelGo;
    private ObjectToBeLerped mapGo;

    private bool menuShown = false;

    void Start()
    {
        sidePanelGo = GameObject.Find("Side Panel").GetComponent<ObjectToBeLerped>();
        mapGo = GameObject.Find("Map").GetComponent<ObjectToBeLerped>();
        menuGo = GameObject.Find("MENU").GetComponent<ObjectToBeLerped>();
    }

    public void settingsButtonPressed()
    {
        changeMenuVisibility(!menuShown);
    }

    public void changeMenuVisibility(bool b)
    {
        menuShown = b;

        moveObjects();
    }

    public void moveObjects()
    {
        if(menuShown)
        {
            shade.GoToStage("fadeIn");
            menuGo.GoToStage("menuOn");
            mapGo.GoToStage("menuOn");
            sidePanelGo.GoToStage("menuOn");
        } else
        {
            shade.GoToStage("fadeOut");
            menuGo.GoToStage("menuOff");
            mapGo.GoToStage("menuOff");
            sidePanelGo.GoToStage("menuOff");
        }
    }
}
