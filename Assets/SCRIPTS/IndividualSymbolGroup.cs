using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualSymbolGroup : MonoBehaviour
{
    public OverlappingRegion _region;

    public Vector3 showingPosition;
    public Vector3 hidingPosition;

    void Awake()
    {
        hidingPosition = new Vector3(100, 100, 0);
    }
    
    public void setMyRegion(OverlappingRegion or)
    {
        _region = or;
    }

    public void Show()
    {
        transform.position = showingPosition;
    }

    public void Hide()
    {
        transform.position = hidingPosition;
    }

}
