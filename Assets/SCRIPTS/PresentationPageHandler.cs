using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PresentationPageHandler : MonoBehaviour
{
    // CONTROLS THE PRESENTATION PART OF THE APP

    public GameObject valuePage;
    public GameObject UI;
    private Vector3 initialPos;
    private Vector3 hiddenPos;

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

    public void PresentationPageButtonClicked()
    {
        // valuePage.SetActive(false);
        valuePage.GetComponent<ValuePageHandler>().Hide();

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

            presentation = true;
        } else
        {
            map.anchoredPosition = initMapPos;
            texture.sprite = transparentImage;

            presentation = false;
        }
    }
}
