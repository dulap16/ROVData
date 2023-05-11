using System.Collections;
using System.Collections.Generic;
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

    // smooth movement
    [SerializeField] [Range(1, 10)] float lerpTime = 4;
    private Color targetColor;
    private Vector2 targetPosition;

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

        targetColor = texture.color;
        lerpTime = 4.5f;
    }

    void Update()
    {
        if (map.anchoredPosition != targetPosition)
        {
            map.anchoredPosition = Vector3.Lerp(map.anchoredPosition, targetPosition, lerpTime * Time.deltaTime);
            texture.color = Color.Lerp(texture.color, targetColor, lerpTime * Time.deltaTime);
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

    public void PresentationMode()
    {
        if(presentation == false)
        {
            targetPosition = presMapPos;
            targetColor.a = 1;

            HideUI();

            presentation = true;
        } else
        {
            targetPosition = initMapPos;
            targetColor.a = 0;

            ShowUI();

            presentation = false;
        }
    }
}
