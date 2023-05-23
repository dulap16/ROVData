using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class JudetChanger : MonoBehaviour
{
    [Serializable]
    public class texture
    {
        public Sprite full;
        public Sprite transp;
        public Vector3 poz;
        public Vector3 scale;
    }

    [Serializable]
    public class judet
    {
        public string nume;
        public GameObject fbx;
        public Vector3 poz;
        public Vector3 scale;
        public Quaternion rotation;
        public texture texture;
    }

    public List<judet> judete;
    Dictionary<string, judet> dict;


    public TMP_Dropdown dd;
    
    public int currInt;
    public string currString;
    public judet currJudet;

    // fbx and texture
    public Material GAL;
    public Material shadow;

    public GameObject blender;
    public SpriteRenderer fullTexture;
    public SpriteRenderer fadedTexture;

    // reset val dropdown
    public Handler h;

    // Start is called before the first frame update
    void Start()
    {
        dict = new Dictionary<string, judet>();
        dd.options.Clear();

        foreach(judet j in judete)
        {
            dict.Add(j.nume, j);

            dd.options.Add(new TMP_Dropdown.OptionData() { text = j.nume }) ;
        }

        dd.onValueChanged.AddListener(delegate { JudetSchimbat(); }) ;
    }

    private void JudetSchimbat()
    {
        string key = dd.options[dd.value].text;
        judet curent = dict[key];

        // change fbx
        foreach (Transform child in blender.transform)
            Destroy(child.gameObject);
        GameObject newJudet = Instantiate(curent.fbx);
        newJudet.transform.position = curent.poz;
        newJudet.name = newJudet.name.Substring(0, newJudet.name.Length);

        foreach (Transform child in newJudet.transform)
        {
            child.transform.localScale = curent.scale;
            if (child.name == "Almas.001" || child.name == "Ghidigeni.001" || child.name == "Insuratei.001")
                child.name = "CADASTRU";
        }

        newJudet.transform.parent = blender.transform;
        foreach (Transform child in newJudet.transform)
        {
            if (child.name == "CADASTRU")
                child.parent = blender.transform;
        }

        // change texture
        fullTexture.sprite = curent.texture.full;
        fadedTexture.sprite = curent.texture.transp;

        // reset val dropdown


        // reset datasets


        // reset display mode to color


        // empty points group and symbols group


        // run symbols again


        // reset limits


        // arata cadastru


    }
}
