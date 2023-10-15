using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Search;
using UnityEngine;

public class NewUIHandler : MonoBehaviour
{
    /*Main Page*/
    public GameObject selectedRegionPanel;
    public GameObject onTexturePanel;
    public GameObject backButton;

    public OverlappingRegion selectedRegion;

    public Handler mainHandler;
    public TMP_Text selectedRegionTMP;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedRegionChanged()
    {
        selectedRegion = mainHandler.current;
        selectedRegionTMP.text = selectedRegion.CapitaliseForPreview(selectedRegion.name);
    }

    
}
