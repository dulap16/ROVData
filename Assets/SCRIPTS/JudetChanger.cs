using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    public PhysicMaterial phys;

    public GameObject blender;
    public SpriteRenderer fullTexture;
    public SpriteRenderer fadedTexture;

    // reset when judet changed
    public Handler h;
    public DisplayModeHandler dmHandler;
    public SymbolManager sm;
    public DatasetHandler dh;

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

        dh.judete = returnJudete();

        currJudet = dict["Galati"];
        currString = currJudet.nume;
        h.curentJudet = currString;

        dd.onValueChanged.AddListener(delegate { JudetSchimbat(); }) ;
    }

    private void JudetSchimbat()
    {
        string key = dd.options[dd.value].text;
        currJudet = dict[key];
        currString = currJudet.nume;

        // change fbx
        foreach (Transform child in blender.transform)
            Destroy(child.gameObject);
        GameObject newJudet = Instantiate(currJudet.fbx);
        newJudet.transform.position = currJudet.poz;
        newJudet.name = currJudet.nume;

        foreach (Transform child in newJudet.transform)
        {
            child.transform.localScale = currJudet.scale;
            if (child.name == "Almas.001" || child.name == "Ghidigeni.001" || child.name == "Insuratei.001")
                child.name = "CADASTRU";
        }

        newJudet.transform.parent = blender.transform;
        foreach (Transform child in newJudet.transform)
        {
            if (child.name == "CADASTRU")
            {
                child.parent = blender.transform;
                child.GetComponent<Renderer>().material = shadow;
            } else
            {
                // this might be needed to happen later
                child.AddComponent<OverlappingRegion>();
                child.AddComponent<MeshCollider>();
                child.GetComponent<Renderer>().material = GAL;
                child.GetComponent<MeshCollider>().material = phys;
            }
        }

        // change texture
        fullTexture.sprite = currJudet.texture.full;
        fadedTexture.sprite = currJudet.texture.transp;

        // reset val dropdown
        h.JudetChanged();
        h.curentJudet = currJudet.nume;

        // reset datasets
        dh.JudetChanged();

        // reset display mode to color
        // empty points group and symbols group
        dmHandler.JudetChanged();

        // run symbols again
        sm.JudetChanged();

        h.AssignRandomValues();
    }

    public List<string> returnJudete()
    {
        return dict.Keys.ToList();
    }
}
