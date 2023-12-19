using Assets.SCRIPTS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ZoomManager : MonoBehaviour
{
    public GameObject map;
    [SerializeField] private Vector3 mapInitCenter;
    public Vector3 mapPresCenter;
    private Vector3 mapOriginalCenter;
    private Vector3 mapInitScale;

    [SerializeField] private byte zoomLevel = 0;
    public float timeBetweenZooms = 2;
    private float timeSinceLastZoom = 0;

    public float zoomRatio = 1.25f;

    public Vector3 prevMousePosition;

    public GameObject mapBoundriesObj;
    public GameObject mapNorthObj;
    public GameObject mapSouthObj;
    public GameObject mapEastObj;
    public GameObject mapWestObj;
    
    public Boundries mapBoundries;

    public Boundries cameraBoundries;
    public float cameraPresWest;
    public float cameraNoPresWest;


    private Vector3 targetCenter;
    private Vector3 targetScale;
    public float lerpTime;

    void Start()
    {
        cameraNoPresWest = cameraBoundries.west;

        mapOriginalCenter = map.transform.position;
        changeMapInitCenter(mapOriginalCenter);
        mapInitScale = map.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float scrolling = Input.mouseScrollDelta.y;

        if (ableToZoom() && scrolling != 0)
        {
            scrolled(scrolling);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse2) && zoomLevel > 0)
        {
            lerpTime = 21;

            Vector3 diff = getMouse() - prevMousePosition;
            diff.z = 0;
            Vector3 newCenter = new Vector3(diff.x, diff.y, 0) + map.transform.position;

            updateMapCenter(newCenter);
        }
        else lerpTime = 8;

        if(zoomLevel != 0)
            rectifyMapToBeWithinCamera();

        //lerpIfCase();

        prevMousePosition = getMouse();
    }

    private void lerpIfCase()
    {
        if (targetCenter.x != map.transform.position.x || targetCenter.y != map.transform.position.y || targetScale.x != map.transform.localScale.x || targetScale.y != map.transform.localScale.y)
        {
            lerpMapPosition();
            lerpMapScale();
        }
    }

    private void lerpMapPosition()
    {
        map.transform.position = Vector3.Lerp(map.transform.position, targetCenter, lerpTime * Time.deltaTime);
    }

    private void lerpMapScale()
    {
        map.transform.localScale = Vector3.Lerp(map.transform.localScale, targetScale, lerpTime * Time.deltaTime);
    }

    private bool isMapWithinCamera()
    {
        mapBoundries.north = mapBoundriesObj.transform.TransformPoint(mapNorthObj.transform.localPosition).y;
        mapBoundries.south = mapBoundriesObj.transform.TransformPoint(mapSouthObj.transform.localPosition).y;
        mapBoundries.east = mapBoundriesObj.transform.TransformPoint(mapEastObj.transform.localPosition).x;
        mapBoundries.west = mapBoundriesObj.transform.TransformPoint(mapWestObj.transform.localPosition).x;

        return cameraBoundries.isThisWithinBoundries(mapBoundries);
    }

    private void scrolled(float axis)
    {
        if (axis > 0)
        {
            if(zoomLevel < 4)
                zoomInOnMouse(zoomRatio);
        }
        if (axis < 0)
        {
            if (zoomLevel > 0)
                zoomOutFromMouse(zoomRatio);
        }

        timeSinceLastZoom = Time.time;
    }

    private void zoomInOnMouse(float ratio)
    {
        zoomInOnPosition(getMouse(), ratio);
    }
    private void zoomOutFromMouse(float ratio)
    {
        zoomOutFromPosition(getMouse(), ratio);
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

    public void resetToOriginal()
    {
        zoomLevel = 0;

        updateMapCenter(mapInitCenter);
        changeMapScale(mapInitScale);
        //targetScale = mapInitScale;
    }

    private void multiplyMapScale(float howMuch)
    {
        //targetScale = map.transform.localScale * howMuch;
        changeMapScale(map.transform.localScale * howMuch);
    }

    private void changeMapScale(Vector3 newScale)
    {
        newScale.z = 1;
        map.transform.localScale = newScale;
    }

    public void updateMapCenter(Vector3 newCenter)
    {
        newCenter.z = 1;
        //targetCenter = newCenter;
        map.transform.position = newCenter;
    }

    public void changeMapInitCenter(Vector3 newCenter)
    {
        mapInitCenter = newCenter;
    }

    public void presentationModeActivated()
    {
        mapInitCenter = mapPresCenter;
        setWestCameraBoundries(cameraPresWest);
    }

    public void presentaionModeDeactivated()
    {
        mapInitCenter = mapOriginalCenter;
        setWestCameraBoundries(cameraNoPresWest);
    }

    private bool ableToZoom()
    {
        return Time.time - timeSinceLastZoom > timeBetweenZooms;
    }

    private Vector3 getMouse()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void rectifyMapToBeWithinCamera()
    {
        Vector3 newCenter = map.transform.position;

        if (!isMapWithinCamera())
        {
            if (cameraBoundries.north > mapBoundries.north)
                newCenter.y = newCenter.y + (cameraBoundries.north - mapBoundries.north);
            if (cameraBoundries.south < mapBoundries.south)
                newCenter.y = newCenter.y + (cameraBoundries.south - mapBoundries.south);
            if (cameraBoundries.east > mapBoundries.east)
                newCenter.x = newCenter.x + (cameraBoundries.east - mapBoundries.east);
            if (cameraBoundries.west < mapBoundries.west)
                newCenter.x = newCenter.x + (cameraBoundries.west - mapBoundries.west);

            updateMapCenter(newCenter);
        }
    }

    public void setWestCameraBoundries(float newBoundries)
    {
        cameraBoundries.west = newBoundries;
    }
}
