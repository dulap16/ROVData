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
    public VisualsManager visualsManager;
    public GameObject pointsGroup;

    public string First;

    IEnumerator waiter()
    {
        yield return new WaitUntil(() => blender.transform.GetChild(0).name == currString);
        sm.Generate();
        yield return new WaitUntil(() => sm.generated == true);
        
        h.DelegateSymbols();
        sm.generated = false;

        yield return new WaitForSecondsRealtime(1);
        dh.JudetChanged();

        dmHandler.modeChanged(0);
    }

    IEnumerator waiter(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        h.DelegateSymbols();
        sm.generated = false;
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
        
        if(currString != "Braila")
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

        h.Start();
        visualsManager.cadastre = cad.gameObject;
        visualsManager.JudetChanged();
        
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
        dmHandler.ResetPositions();

        string key = dd.options[dd.value].text;
        currJudet = dict[key];
        currString = currJudet.nume;
        h.curentJudet = currString;

        foreach (Transform child in pointsGroup.transform)
            Destroy(child.gameObject);

        // copied from JudetSchimbat()
        foreach (Transform child in blender.transform)
            Destroy(child.gameObject);

        GameObject newJudet = Instantiate(currJudet.fbx);

        if (currString != "Braila")
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
            child.gameObject.layer = 8;
            child.AddComponent<MeshCollider>();
            child.GetComponent<Renderer>().material = GAL;
            child.GetComponent<MeshCollider>().material = phys;

            // this might be needed to happen later
            child.AddComponent<OverlappingRegion>();
            
        }

        h.Start();
        visualsManager.JudetChanged();

        h.judetGO = newJudet;

        fullTexture.sprite = currJudet.texture.full;
        fadedTexture.sprite = currJudet.texture.transp;
        fullTexture.transform.localPosition = fadedTexture.transform.localPosition = currJudet.texture.position;

        StartCoroutine(waiter());
    }

    public List<string> returnJudete()
    {
        return dict.Keys.ToList();
    }
}
