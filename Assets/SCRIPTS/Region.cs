using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    /* USELESS
    public GameObject indicator;
    private Vector3 center;
    // Start is called before the first frame update
    void Awake()
    {
        center = GetComponent<Renderer>().bounds.center;
        GameObject newObject = (GameObject)Instantiate(indicator, new Vector3(center.x, center.y, -12000), Quaternion.identity);
        if (name.Contains("."))
            name = name.Substring(0, name.Length - 4);

        newObject.GetComponent<Indicator>().regionsName = name;
        newObject.GetComponent<Indicator>().parent = this.gameObject;
    }
    */
}
