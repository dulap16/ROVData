using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class JudetChanger : MonoBehaviour
{
    public TMP_Dropdown dd;

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
        public Vector3 rotation;
        public texture texture;
    }

    public List<judet> judete;

    // Start is called before the first frame update
    void Start()
    {
        dd.options.Clear();

        foreach(judet j in judete)
        {
            dd.options.Add(new TMP_Dropdown.OptionData() { text = j.nume }) ;
        }

        dd.onValueChanged.AddListener(delegate { JudetSchimbat(); }) ;
    }

    private void JudetSchimbat()
    {

    }
}
