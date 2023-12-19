using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

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
    public GameObject individualSymbolGroup;
    public ZoomManager zm;

    public string First;

    public TextureToggle tt;

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1f);

        h.makeSymbolsForEveryone();

        dh.JudetChanged();

        dmHandler.modeChanged(0);

        h.changeRegionAspectBasedOnLimits();

        h.EmptySamples();
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
        dd.value = dd.options.IndexOf(new TMP_Dropdown.OptionData(First));

        currJudet = dict[first];
        currString = first;
        h.curentJudet = first;

        // copied from JudetSchimbat()
        foreach (Transform child in individualSymbolGroup.transform)
            Destroy(child.gameObject);

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
            string Name = child.name;
            if (Name.Contains("."))
                Name = Name.Substring(0, Name.Length - 4);
            Name = Name.ToLower();

            GameObject newGroup = new GameObject(Name);
            newGroup.transform.parent = individualSymbolGroup.transform;
            newGroup.AddComponent<IndividualSymbolGroup>();

            
            // this might be needed to happen later
            child.AddComponent<OverlappingRegion>();
            child.gameObject.layer = 8;
            child.AddComponent<MeshCollider>();
            child.GetComponent<Renderer>().material = GAL;
            child.GetComponent<MeshCollider>().material = phys;

            child.GetComponent<OverlappingRegion>().isg = newGroup.GetComponent<IndividualSymbolGroup>();
            newGroup.GetComponent<IndividualSymbolGroup>().setMyRegion(child.GetComponent<OverlappingRegion>());
        }

        h.Start();
        
        visualsManager.cadastre = cad.gameObject;
        visualsManager.JudetChanged();
        
        h.judetGO = newJudet;

        fullTexture.sprite = currJudet.texture.full;
        fadedTexture.sprite = currJudet.texture.transp;
        fullTexture.transform.localPosition = fadedTexture.transform.localPosition = currJudet.texture.position;
    }

    private void JudetSchimbat()
    {
        dmHandler.ResetPositions();
        h.limitsManager.Reset();
        h.zm.resetToOriginal();

        string key = dd.options[dd.value].text;
        currJudet = dict[key];
        currString = currJudet.nume;
        h.curentJudet = currString;

        foreach (Transform child in pointsGroup.transform)
            Destroy(child.gameObject);

        // copied from JudetSchimbat()
        foreach (Transform child in individualSymbolGroup.transform)
            Destroy(child.gameObject);

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
            string Name = child.name;
            if (Name.Contains("."))
                Name = Name.Substring(0, Name.Length - 4);
            Name = Name.ToLower();

            GameObject newGroup = new GameObject(Name);
            newGroup.transform.parent = individualSymbolGroup.transform;
            newGroup.AddComponent<IndividualSymbolGroup>();

            
            child.gameObject.layer = 8;
            child.AddComponent<MeshCollider>();
            child.GetComponent<Renderer>().material = GAL;
            child.GetComponent<MeshCollider>().material = phys;

            // this might be needed to happen later
            child.AddComponent<OverlappingRegion>();
            
        }

        h.Start();
        h.updateLimitVariables();
        visualsManager.JudetChanged();

        h.judetGO = newJudet;

        fullTexture.sprite = currJudet.texture.full;
        fadedTexture.sprite = currJudet.texture.transp;
        fullTexture.transform.localPosition = fadedTexture.transform.localPosition = currJudet.texture.position;

        tt.changeTexture(fullTexture);

        StartCoroutine(waiter());
    }

    public List<string> returnJudete()
    {
        return dict.Keys.ToList();
    }
}
