using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public Vector3 rotation;
        public texture texture;
    }

    public List<judet> judete;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.streamingAssetsPath);
        Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
