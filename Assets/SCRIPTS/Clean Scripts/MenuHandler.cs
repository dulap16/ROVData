using Assets.SCRIPTS.Start_Page;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public ObjectToBeLerped shade;
    private ObjectToBeLerped settingsOTBL; 
    private ObjectToBeLerped mainPanelOTBL;
    private ObjectToBeLerped mapOTBL;

    private bool settingsShown = false;


    // hiding the side panel
    private ObjectToBeLerped sidePanelOTBL;
    private ObjectToBeLerped panelToggleOTBL;

    private bool isPresentation = false;

    public GameObject westCameraBoundries;

    public ZoomManager zm;

    public Vector3 noPresCenter;
    public Vector3 presCenter;

    void Start()
    {
        mainPanelOTBL = GameObject.Find("Main Panel").GetComponent<ObjectToBeLerped>();
        mapOTBL = GameObject.Find("Map").GetComponent<ObjectToBeLerped>();
        settingsOTBL = GameObject.Find("Settings Panel").GetComponent<ObjectToBeLerped>();

        noPresCenter = mapOTBL.transform.TransformPoint(noPresCenter);
        presCenter = mapOTBL.transform.TransformPoint(presCenter);
            
        sidePanelOTBL = GameObject.Find("Separate side").GetComponent<ObjectToBeLerped>();
        panelToggleOTBL = GameObject.Find("Toggle Panel Button").GetComponent<ObjectToBeLerped>();
    }

    public void settingsButtonPressed()
    {
        changeMenuVisibility(!settingsShown);
    }

    public void changeMenuVisibility(bool b)
    {
        settingsShown = b;

        moveSettings();
    }

    public void moveSettings()
    {
        if(settingsShown)
        {
            settingsOTBL.GoToStage("settingsOn");
            mainPanelOTBL.GoToStage("settingsOn");
        } else
        {
            settingsOTBL.GoToStage("settingsOff");
            mainPanelOTBL.GoToStage("settingsOff");
        }
    }

    public void togglePresPressed()
    {
        isPresentation = !isPresentation;

        moveForPresentation();
    }

    public void moveForPresentation()
    {
        if(isPresentation)
        {
            mapOTBL.GoToStage("menuOff");
            panelToggleOTBL.GoToStage("menuOff");
            sidePanelOTBL.GoToStage("menuOff");

            zm.presentationModeActivated();
            zm.setWestCameraBoundries(-18.88f);
        } else
        {
            mapOTBL.GoToStage("menuOn");
            panelToggleOTBL.GoToStage("menuOn");
            sidePanelOTBL.GoToStage("menuOn");

            zm.presentaionModeDeactivated();
            zm.setWestCameraBoundries(-12);
        }
    }
}
