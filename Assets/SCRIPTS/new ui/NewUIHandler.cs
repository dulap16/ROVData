using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class NewUIHandler : MonoBehaviour
{
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
