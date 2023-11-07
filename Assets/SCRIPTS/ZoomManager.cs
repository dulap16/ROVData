using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManager : MonoBehaviour
{
    public GameObject map;
    private Vector3 mapInitCenter;
    private Vector3 mapInitScale;

    private byte zoomlevel = 0;
    public float timeBetweenZooms = 2;
    private float timeSinceLastZoom = 0;

    public float zoomRatio = 2;

    void Start()
    {
        mapInitCenter = map.transform.position;
        mapInitScale = map.transform.localScale;
    }

}
