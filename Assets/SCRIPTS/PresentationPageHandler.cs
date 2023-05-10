using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PresentationPageHandler : MonoBehaviour
{
    // CONTROLS THE PRESENTATION PART OF THE APP

    public GameObject valuePage;
    private Vector3 initialPos;
    private Vector3 hiddenPos;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        hiddenPos = new Vector3(500, 0, 0);

        Hide();
    }

    public void Show()
    {
        transform.localPosition = initialPos;
    }

    public void Hide()
    {
        transform.localPosition = hiddenPos;
    }

    public void PresentationPageButtonClicked()
    {
        // valuePage.SetActive(false);
        valuePage.GetComponent<ValuePageHandler>().Hide();

        // GetComponent<GameObject>().SetActive(true);
        Show();
    }

    public void PresentationMode()
    {
        ;
    }
}
