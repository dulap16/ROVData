using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    // SELECTION
    public bool selected;

    // PARENT DETAILS
    public GameObject tagObject;
    public string regionNameOnTag;
    public string regionsName;
    public GameObject parent;
    public byte alphaValue;
    public int value;

    private OverlappingRegion parentScript;
    public new Tag tag;

    // APPEARANCE BASED ON VALUE
    private float minScale = 0.15f;
    private float maxScale = 0.7f;
    private float currentScale;


    private Handler h;
    public CursorFollower cf;

    // APPEARANCE CONTROL - SIZE
    public GameObject outline;
    public Vector3 finalScale;
    public Vector3 initialScale;
    public Vector3 targetScale;
    public float overflow = 2f;
    public float scaleRatio = 0.05f;

    // APPEARANCE CONTROL - COLOR
    private new Renderer renderer;

    private Color initialColor;
    private Color finalColor;
    public float colorRatio = 10;

    public Color32 deselectionColor;

    public float lerpTime = 0.1f;

    public TMP_Text insideText;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = parent.GetComponent<OverlappingRegion>();

        tag = tagObject.GetComponent<Tag>();

        regionNameOnTag = CapitaliseForPreview(regionsName);
        tag.ChangeText(regionNameOnTag + " : " + value.ToString());
        tag.MakeInvisible();

        h = GameObject.Find("Handler").GetComponent<Handler>();

        selected = false;
        changeScaleAccordingToValue(value);
        initialScale = outline.transform.localScale;
        targetScale = initialScale;
        finalScale = new Vector3(initialScale.x + overflow, initialScale.y + overflow, initialScale.z);


        renderer = GetComponent<Renderer>();
        initialColor = renderer.material.color;

        finalColor = initialColor;
        finalColor.a = 0.5f;

        deselectionColor = new Color32(79, 74, 74, 255);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Update()
    {
        if(outline.transform.localScale != targetScale)
        {
            outline.transform.localScale = Vector3.Lerp(outline.transform.localScale, targetScale, lerpTime * Time.deltaTime);
        }
    }

    public void ChangeTag(string t)
    {
        tag.ChangeText(t);
    }

    public void ChangeValue(int newValue)
    {
        value = newValue;
        ChangeTag(regionNameOnTag + " : " + value.ToString());
        changeScaleAccordingToValue(newValue);
    }

    public void HideOutline()
    {
        targetScale = initialScale;
    }

    public void ShowOutline()
    {
        targetScale = finalScale;
    }

    public void FadeOut()
    {
        renderer.material.color = deselectionColor;
    }

    public void FadeIn()
    {
        renderer.material.color = initialColor;
    }

    // visuals
    public void Selected()
    {
        selected = true;

        if(h.isReset())
        {
            ShowOutline();
        }

        MakeNormalSized();
    }

    public void Deselected()
    {
        selected = false;

        if (h.isReset())
        {
            HideOutline();
        } else
        {
            if (!IsWithinLimits())
                OutsideOfLimits();
        }
    }

    private void MakeSmallSized()
    {
        changeScale(new Vector3(minScale, minScale, 1));
    }

    private void MakeNormalSized()
    {
        changeScale(new Vector3(currentScale, currentScale, 1));
    }

    public void changeScale(Vector3 newScale)
    {
        transform.localScale = newScale;
    }

    public void changeScaleAccordingToValue(int v)
    {
        currentScale = ((float)v / h.max) * (maxScale - minScale) + minScale;
        changeScale(new Vector3(currentScale, currentScale, 1));
    }

    public string CapitaliseForPreview(string name)
    {
        char[] newName = new char[name.Length];

        for(int i = 0; i < name.Length; i++)
        {
            if (i == 0 || (i > 0 && name[i - 1] == ' '))
            {
                newName[i] = (char)((int)name[i] + 'A' - 'a');
            }
            else newName[i] = name[i];
        }

        return new string(newName);
    }

    public bool IsWithinLimits()
    {
        return parentScript.CheckWithinLimits(value);
    }

    public void Standby()
    {
        FadeIn();
        HideOutline();
        MakeNormalSized();
    }

    public void WithinLimits()
    {
        FadeIn();
        ShowOutline();
        MakeNormalSized();
    }

    public void OutsideOfLimits()
    {
        FadeOut();
        HideOutline();
        MakeSmallSized();
    }


    // INSIDE TEXT

    private bool isInsideTextVisible = true;
    private string FormatValue(int value)
    {
        return NrFormatter(value);
    }

    private string NrFormatter(int nr)
    {
        if (nr < 1000)
            return nr.ToString();

        if (nr < 1000000)
        {
            float thousand = (float)nr / 1000f;
            thousand = Mathf.Round(thousand * 100f) / 100f;
            return thousand + "K";
        }

        float million = (float)nr / 1000000f;
        million = Mathf.Round(million * 100f) / 100f;
        return million + "M";
    }

    private void changeInsideText(string newText)
    {
        insideText.text = newText;
    }

}
