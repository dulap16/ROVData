using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
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

    public Vector3 hiddenPos;

    [Serializable]
    public class Segment
    {
        public String name;
        public GameObject _go;
        public Vector3 initPos;
        private Vector3 hiddenPos = new Vector3(1000, 1000, 0);

        public void show(bool b)
        {
            if (b)
                _go.transform.localPosition = initPos;
            if (b)
                _go.transform.localPosition = hiddenPos;
        }

        public void _init()
        {
            initPos = _go.transform.localPosition;
            
        }

        public void _init(Vector3 pos)
        {
            initPos = pos;
        }
    }

    [SerializeField] public List<Segment> segments;
    
    // Start is called before the first frame update
    void Start()
    {
        InitSegments();
    }

    public void InitSegments()
    {
        foreach(Segment s in segments)
        {
            s._init();
        }
    }

    public void SelectedRegionChanged()
    {
        selectedRegion = mainHandler.current;
        mainHandler.Selected(selectedRegion);
    }
}
