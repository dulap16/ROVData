using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PresentationPageHandler : MonoBehaviour
{
    // CONTROLS THE PRESENTATION PART OF THE APP

    public ValuePageHandler valuePage;
    private Vector3 initialPos;
    private Vector3 hiddenPos;

    public GameObject UI;
    private Vector3 uiinit;
    private Vector3 uifinal;

    public bool shown = false;

    // PRESENTATION MODE
    public RectTransform map;
    public SpriteRenderer texture;
    public Sprite fullImage;
    public Sprite transparentImage;
    public bool presentation = false;

    private Vector3 initMapPos;
    private Vector3 presMapPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        hiddenPos = new Vector3(500, 0, 0);

        uiinit = UI.transform.localPosition;
        uifinal = new Vector3(500, 0, 0);

        Hide();

        initMapPos = map.anchoredPosition;
        Debug.Log(initMapPos);
        presMapPos = new Vector3(827, 293, 2.172865f);
    }

    public void Show()
    {
        transform.localPosition = initialPos;
    }

    public void Hide()
    {
        transform.localPosition = hiddenPos;
    }

    public void ShowUI()
    {
        UI.transform.localPosition = uiinit;
    }

    public void HideUI()
    {
        UI.transform.localPosition = uifinal;
    }

    public void PresentationPageButtonClicked()
    {
        // valuePage.SetActive(false);
        valuePage.Hide();

        // GetComponent<GameObject>().SetActive(true);
        Show();
        shown = true;
    }

    public void PresentationMode()
    {
        if(presentation == false)
        {
            map.anchoredPosition = presMapPos;
            texture.sprite = fullImage;

            HideUI();

            presentation = true;
        } else
        {
            map.anchoredPosition = initMapPos;
            texture.sprite = transparentImage;

            ShowUI();

            presentation = false;
        }
    }
}
