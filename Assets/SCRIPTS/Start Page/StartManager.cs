using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SCRIPTS.Start_Page;

public class StartManager : MonoBehaviour
{
    [SerializeField] public Dictionary<GameObject, ObjectToBeLerped> lerpedObjects;

    /// GAMEOBJECTS, SPRITE RENDERERS AND COLORS
    public GameObject lowerCover;
    public GameObject upperCover;
    public GameObject graphCover;
    public GameObject logo;

    void Start()
    {
        lerpedObjects = new Dictionary<GameObject, ObjectToBeLerped>();
        lerpedObjects.Add(lowerCover, lowerCover.GetComponent<ObjectToBeLerped>());
        lerpedObjects.Add(upperCover, upperCover.GetComponent<ObjectToBeLerped>());
        lerpedObjects.Add(graphCover, graphCover.GetComponent<ObjectToBeLerped>());
        lerpedObjects.Add(logo,  logo.GetComponent<ObjectToBeLerped>());

        StartLerpingAllObjects();
    }

    void Update()
    {
        if(Input.GetKeyDown("space"))
            Restart();
    }

    public void StartLerpingObject(GameObject go)
    {
        lerpedObjects[go].StartLerping();
    }

    public void StartLerpingAllObjects()
    {
        foreach (ObjectToBeLerped obj in lerpedObjects.Values)
        {
            obj.StartLerping();
        }
    }

    public void Restart()
    {
        foreach(ObjectToBeLerped obj in lerpedObjects.Values)
        {
            obj.Restart();
        }
    }
}
