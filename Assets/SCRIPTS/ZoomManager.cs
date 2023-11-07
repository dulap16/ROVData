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

    private void scrolled(float axis)
    {
        if (axis > 0)
        {
            Debug.Log("Zoom in!");

            zoomOnMouse(zoomRatio);
        }
        if (axis < 0)
        {
            resetToOriginal();
        }

            timeSinceLastZoom = Time.time;
    }

    private void zoomOnMouse(float ratio)
    {
        zoomOnPosition(getMouse(), ratio);
    }

    private void zoomOnPosition(Vector3 pos, float ratio)
    {
        float dist = Vector3.Distance(pos, mapInitCenter);
    }

    private void resetToOriginal()
    {
        map.transform.position = mapInitCenter;
        map.transform.localScale = mapInitScale;
    }

    private bool ableToZoom()
    {
        return Time.time - timeSinceLastZoom > timeBetweenZooms;
    }

    private Vector3 getMouse()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
