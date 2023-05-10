using System.Collections;
using System.Collections.Generic;
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
    private GameObject overlapMap;
    public Tag tag;

    // APPEARANCE BASED ON VALUE
    public float minScale = 0.15f;
    public float maxScale = 0.7f;

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
    private Renderer renderer;
    private Color targetColor;
    private Color initialColor;
    private Color finalColor;
    public float colorRatio = 10;

    public Color32 deselectionColor;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = parent.GetComponent<OverlappingRegion>();

        tag = tagObject.GetComponent<Tag>();

        regionNameOnTag = CapitaliseForPreview(regionsName);
        tag.ChangeText(regionNameOnTag + " : " + value);
        tag.MakeInvisible();

        h = GameObject.Find("Handler").GetComponent<Handler>();

        selected = false;
        transform.localScale = NewScale(value);
        initialScale = outline.transform.localScale;
        targetScale = initialScale;
        finalScale = new Vector3(initialScale.x + overflow, initialScale.y + overflow, initialScale.z);


        renderer = GetComponent<Renderer>();
        initialColor = renderer.material.color;
        targetColor = initialColor;
        finalColor = initialColor;
        finalColor.a = 0.5f;

        deselectionColor = new Color32(79, 74, 74, 255);

        Debug.Log(initialColor);
    }

    void Update()
    {
        if(transform.localScale != targetScale)
        {
            ChangeShadowScale();
        }

        /*if(renderer.material.color != targetColor)
        {
            ChangeAlpha();
        }*/
    }

    public void OnMouseOver()
    {
        if (h.selectedValuesOnly == false)
           ShowOutline();
        if (cf.shown == false)
            cf.MakeVisible();
        if (cf.GetText() != tag.GetText())
            cf.ChangeText(tag.GetText());
    }

    public void OnMouseExit()
    {
        if (selected == false && h.selectedValuesOnly == false)
        {
            parentScript.MakeInvisible(150, false);
            HideOutline();
        }

        /*tag.MakeInvisible();*/

        cf.MakeInvisible();
    }

    public void ChangeShadowScale()
    {
        if (Mathf.Abs(outline.transform.localScale.x - targetScale.x) < Mathf.Abs(scaleRatio))
        {
            outline.transform.localScale = targetScale;
            return;
        }   

        if (outline.transform.localScale.x > targetScale.x)
            scaleRatio = Mathf.Abs(scaleRatio) * -1;
        else scaleRatio = Mathf.Abs(scaleRatio);

        Vector3 nextScale = new Vector3(outline.transform.localScale.x + scaleRatio, outline.transform.localScale.y + scaleRatio, outline.transform.localScale.z);
        outline.transform.localScale = nextScale;
    }

    public void ChangeAlpha()
    {
        Color color = renderer.material.color;
        if(Mathf.Abs(color.a - targetColor.a) < Mathf.Abs(colorRatio))
        {
            renderer.material.color = targetColor;
            return;
        }

        if (color.a > targetColor.a)
            colorRatio = Mathf.Abs(colorRatio) * -1;
        else colorRatio = Mathf.Abs(colorRatio);

        Color nextColor = color;
        nextColor.a = nextColor.a + colorRatio;
        renderer.material.color = nextColor;
    }

    public void ChangeValue(int newValue)
    {
        value = newValue;
        tag.ChangeText(regionNameOnTag + " : " + value);
        transform.localScale = NewScale(newValue);
    }

    private void OnMouseDown()
    {
        h.ChangeOption(regionNameOnTag);

        if (h.selectedValuesOnly == false)
            h.Selected(parentScript);
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

    public Vector3 NewScale(int v)
    {
        float currentScale = ((float)v / 10000f) * (maxScale - minScale) + minScale;
        return new Vector3(currentScale, currentScale, 1);
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
}
