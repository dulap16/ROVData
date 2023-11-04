using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualsManager : MonoBehaviour
{
    public Handler handler;

    // CADASTRE SETTINGS
    public GameObject cadastre = null;
    private Material cadMat;

    public Slider cadOpacSlider;
    public TMP_Text cadOpacTag;


    // PRESENTATION MODE
    public RectTransform map;
    public SpriteRenderer texture;
    public Sprite fullImage;
    public Sprite transparentImage;
    public bool presentation = false;

    private Vector3 initMapPos;
    public Vector3 presMapPos;

    // TITLE
    public TMP_InputField titleIF;
    public TMP_Text title;

    void Start()
    {
        handler = GameObject.Find("Handler").GetComponent<Handler>();
        cadMat = cadastre.GetComponent<Renderer>().material;
    }

    public void JudetChanged()
    {
        cadMat = cadastre.GetComponent<Renderer>().material;

        titleIF.text = handler.curentJudet;
        title.text = handler.curentJudet;
    }

    // CADASTRE
    public void SliderSlid()
    {
        float val = Mathf.Round(cadOpacSlider.normalizedValue * 100f) / 100f;
        cadOpacTag.text = val.ToString();
        setCadOpac(val);
    }


    public void setCadOpac(float val)
    {
        Color c = cadMat.color;
        c.a = val;
        cadMat.color = c;
    }



    // TITLE
    public void titleIFChanged()
    {
        changeTitle(titleIF.text);
    }

    public void changeTitle(string t) 
    {
        title.text = t;
    }
}
