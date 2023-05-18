using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentationPageHandler : MonoBehaviour
{
    // CONTROLS THE PRESENTATION PART OF THE APP

    public ValuePageHandler valuePage;
    private Vector3 initialPos;
    private Vector3 hiddenPos;

    public TMP_InputField titleIF;
    public TMP_Text title;
    public Color targetTextColor;
    public Color targetBorderColor;

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

    // smooth movement
    [SerializeField] [Range(1, 10)] float lerpTime = 4;
    private Color targetColor;
    private Vector2 targetPosition;

    // CADASTRE 
    public GameObject cadastre;
    private Material cadMat;
    private Vector3 initCadPos;
    private Vector3 targetCadPos;
    private bool cadShowing = true;

    public Slider cadOpac;
    public TMP_Text cadOpacNr;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        hiddenPos = new Vector3(500, 0, 0);

        uiinit = UI.transform.localPosition;
        uifinal = new Vector3(500, 0, 0);

        Hide();

        initMapPos = map.anchoredPosition;
        targetPosition = initMapPos;
        presMapPos = new Vector3(819, 293, initMapPos.z);

        targetColor = texture.color;
        lerpTime = 4.5f;

        targetTextColor.a = 0;
        targetBorderColor.a = 0;

        titleIF.onValueChanged.AddListener(delegate { ChangeTitle(); });
        cadOpac.onValueChanged.AddListener(delegate { SliderSlid(); });

        // CADASTRE
        initCadPos = cadastre.transform.localPosition;
        targetCadPos = new Vector3(500, 0, 0);

        cadMat = cadastre.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (map.anchoredPosition != targetPosition || title.faceColor != targetTextColor || title.outlineColor != targetBorderColor)
        {
            map.anchoredPosition = Vector3.Lerp(map.anchoredPosition, targetPosition, lerpTime * Time.deltaTime);
            texture.color = Color.Lerp(texture.color, targetColor, lerpTime * Time.deltaTime);
            title.faceColor = Color.Lerp(title.faceColor, targetTextColor, lerpTime * Time.deltaTime);
            title.outlineColor = Color.Lerp(title.outlineColor, targetBorderColor, lerpTime * Time.deltaTime);
        }
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

    public void ShowCadastrePressed()
    {
        if (cadShowing == false)
        {
            cadastre.transform.localPosition = initCadPos;
            cadShowing = true;
        }
        else
        {
            cadastre.transform.localPosition = targetCadPos;
            cadShowing = false;
        }
    }

    public void PresentationMode()
    {
        if(presentation == false)
        {
            targetPosition = presMapPos;
            targetColor.a = 1;
            targetBorderColor.a = 0.5f;
            targetTextColor.a = 1;

            HideUI();

            presentation = true;
        } else
        {
            targetPosition = initMapPos;
            targetColor.a = 0;

            targetBorderColor.a = 0;
            targetTextColor.a = 0;

            ShowUI();

            presentation = false;
        }
    }

    public void SliderSlid()
    {
        float value = Mathf.Round(cadOpac.normalizedValue * 100f) / 100f;
        cadOpacNr.text = value.ToString();
        Color c = cadMat.color;
        c.a = value;
        cadMat.color = c;
    }

    public void ChangeTitle()
    {
        string text = titleIF.text;
        title.text = text;
    }
}
