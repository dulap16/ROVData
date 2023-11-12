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

    public float zoomRatio = 1.25f;

    void Start()
    {
        mapInitCenter = map.transform.position;
        mapInitScale = map.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float scrolling = Input.mouseScrollDelta.y;

        if (ableToZoom() && scrolling != 0)
            scrolled(scrolling);
            
    }

    private void scrolled(float axis)
    {
        if (axis > 0)
        {
            if(zoomlevel < 4)
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

    private void zoomInOnPosition(Vector3 pos, float ratio)
    {
        zoomLevel++;
        Vector3 currentCenter = map.transform.position;

        Vector3 direction = pos - currentCenter;
        Vector3 newCenter = currentCenter + direction * (1 - ratio);

        multiplyMapScale(ratio);
        updateMapCenter(newCenter);
    }

    private void zoomOutFromPosition(Vector3 pos, float ratio)
    {
        zoomLevel--;

        if (zoomLevel == 0)
        {
            resetToOriginal(); 
            return;
        }
        Vector3 currentCenter = map.transform.position;

        Vector3 direction = pos - currentCenter;
        Vector3 newCenter = currentCenter + direction * (1 - 1 / ratio);

        multiplyMapScale((1 / ratio));
        updateMapCenter(newCenter);
    }

    private void resetToOriginal()
    {
        zoomlevel = 0;

        updateMapCenter(mapInitCenter);
        changeMapScale(mapInitScale);
    }

    private void enlargeMapScale(float howMuch)
    {
        changeMapScale(map.transform.localScale * howMuch);
    }

    private void changeMapScale(Vector3 newScale)
    {
        map.transform.localScale = newScale;
    }

    private void updateMapCenter(Vector3 newCenter)
    {
        newCenter.z = 1;
        map.transform.position = newCenter;

        Debug.Log(map.transform.localPosition);
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
