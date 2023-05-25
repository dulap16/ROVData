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
        public Vector3 position;
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
    public GameObject currJudetGO;

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

    public string First;

    IEnumerator waiter(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);

        h.DelegateSymbols();
        sm.generated = false;

        /*foreach (Transform child in currJudetGO.transform)
        {
            OverlappingRegion region = child.GetComponent<OverlappingRegion>();
            region.HideAll();
        }*/
    }

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

        FirstJudet(First);

        dd.onValueChanged.AddListener(delegate { JudetSchimbat(); }) ;
    }

    private void FirstJudet(string first)
    {
        currJudet = dict[first];
        currString = first;
        h.curentJudet = first;

        // copied from JudetSchimbat()
        foreach (Transform child in blender.transform)
            Destroy(child.gameObject);

        GameObject newJudet = Instantiate(currJudet.fbx);
        
        foreach (Transform town in newJudet.transform)
            town.position = new Vector3(town.position.x, town.position.y, 0);

        newJudet.name = currJudet.nume;
        currJudetGO = newJudet;

        foreach (Transform child in newJudet.transform)
        {
            if (child.name == "Almas.001" || child.name == "Ghidigeni.001" || child.name == "Insuratei.001")
                child.name = "CADASTRU";
        }

        newJudet.transform.parent = blender.transform;
        newJudet.transform.localPosition = currJudet.poz;
        newJudet.transform.localScale = currJudet.scale;
        newJudet.transform.rotation = currJudet.rotation;

        Transform cad = newJudet.transform;
        foreach (Transform child in newJudet.transform)
            if (child.name == "CADASTRU")
                cad = child.transform;

        cad.parent = blender.transform;
        cad.GetComponent<Renderer>().material = shadow;

        foreach (Transform child in newJudet.transform)
        {
            Debug.Log(child.name);
            // this might be needed to happen later
            child.AddComponent<OverlappingRegion>();
            child.gameObject.layer = 8;
            child.AddComponent<MeshCollider>();
            child.GetComponent<Renderer>().material = GAL;
            child.GetComponent<MeshCollider>().material = phys;
        }

        h.start();

        
        h.judetGO = newJudet;

        fullTexture.sprite = currJudet.texture.full;
        fadedTexture.sprite = currJudet.texture.transp;
        fullTexture.transform.localPosition = fadedTexture.transform.localPosition = currJudet.texture.position;

        
        sm.Generate();
        while (!sm.generated)
            ;

        StartCoroutine(waiter(1));
        
    }

    private void JudetSchimbat()
    {
        // reset display mode to color
        // empty points group and symbols group
        
        
        dmHandler.JudetChanged();
        

        string key = dd.options[dd.value].text;
        currJudet = dict[key];
        currString = currJudet.nume;

        // change fbx
        foreach (Transform child in blender.transform)
            Destroy(child.gameObject);

        GameObject newJudet = Instantiate(currJudet.fbx);
        newJudet.transform.position = currJudet.poz;
        newJudet.transform.localScale = currJudet.scale;
        foreach (Transform town in newJudet.transform)
            town.position = new Vector3(town.position.x, town.position.y, 0);

        newJudet.name = currJudet.nume;

        
        foreach (Transform child in newJudet.transform)
        {
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
                // child.AddComponent<OverlappingRegion>();
                child.gameObject.layer = 8;
                child.AddComponent<MeshCollider>();
                child.GetComponent<Renderer>().material = GAL;
                child.GetComponent<MeshCollider>().material = phys;
            }
        }

        // change texture
        fullTexture.sprite = currJudet.texture.full;
        fadedTexture.sprite = currJudet.texture.transp;
        fullTexture.transform.localPosition = fadedTexture.transform.localPosition = currJudet.texture.position;

        // reset val dropdown
        // sm.JudetChanged();
        h.JudetChanged();
        h.curentJudet = currJudet.nume;

        // reset datasets
        dh.JudetChanged();

        // run symbols again
        foreach (Transform child in newJudet.transform)
            child.AddComponent<OverlappingRegion>();
        h.DelegateSymbols();

        h.AssignRandomValues();
        
    }

    public List<string> returnJudete()
    {
        return dict.Keys.ToList();
    }
}
