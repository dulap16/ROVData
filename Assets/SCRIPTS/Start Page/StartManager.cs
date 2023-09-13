using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SCRIPTS.Start_Page;

public class StartManager : MonoBehaviour
{
    [SerializeField] public ObjectToBeLerped logo;
    [SerializeField] private List<GameObject> objectList;
    [SerializeField] public Dictionary<GameObject, ObjectToBeLerped> lerpedObjects;

    void Start()
    {
        lerpedObjects = new Dictionary<GameObject, ObjectToBeLerped>();
        AddObjectsToDictionary();

        StartLerpingAllObjects();
    }

    private void AddObjectsToDictionary()
    {
        lerpedObjects = new Dictionary<GameObject, ObjectToBeLerped>();
        foreach (GameObject obj in objectList)
        {
            lerpedObjects.Add(obj, obj.GetComponent<ObjectToBeLerped>());
        }
    }

    void Update()
    {
        if(Input.GetKeyDown("space"))
            Restart();

        if (CheckWhetherLogoFinished())
        {
            Debug.Log("finished");
        }
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

    public bool CheckWhetherLogoFinished()
    {
        return logo.getStageManager().finishedEntireAnimation();
    }

    public void Restart()
    {
        foreach(ObjectToBeLerped obj in lerpedObjects.Values)
        {
            obj.Restart();
        }
    }
}
