using Assets.SCRIPTS.Start_Page;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public ObjectToBeLerped shade;
    public GameObject Menugo;

    private bool menuShown = false;

    public void settingsButtonPressed()
    {
        changeMenuVisibility(!menuShown);
    }

    public void changeMenuVisibility(bool b)
    {
        menuShown = b;

        if (menuShown)
            shade.GoToStage("fadeIn");
        else shade.GoToStage("fadeOut");

        Debug.Log(menuShown);
    }
}
