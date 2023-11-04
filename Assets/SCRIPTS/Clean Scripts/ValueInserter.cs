using Assets.SCRIPTS.Start_Page;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueInserter : MonoBehaviour
{
    public ValueInsertion inserter;

    public Handler handler;

    public ObjectToBeLerped valInputField;
    public ObjectToBeLerped insertValButton;

    public bool isFieldShowing = false;

    void Start()
    {
        inserter = new ValueInsertion();
        handler = (GameObject.Find("Handler")).GetComponent<Handler>();
    }

    public void insertValuesClicked()
    {
        inserter.InsertValuesClicked(handler, valInputField.gameObject.GetComponent<TMP_InputField>());
    }

    public void toggleInsertionField()
    {
        if(isFieldShowing)
        {
            isFieldShowing = false;
            HideInsertion();
        } else
        {
            isFieldShowing = true;
            ShowInsertion();
        }
    }

    public void toggleInsertionField(bool show)
    {
        if (show || isFieldShowing)
            toggleInsertionField();
    }

    public void HideInsertion()
    {
        valInputField.GoToStage("hide");
        insertValButton.GoToStage("hide");
    }

    public void ShowInsertion()
    {
        valInputField.GoToStage("show");
        insertValButton.GoToStage("show");
    }
}
