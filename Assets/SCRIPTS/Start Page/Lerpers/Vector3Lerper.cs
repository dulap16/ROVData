using Assets.SCRIPTS.Start_Page;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector3Lerper : Lerper
{
    [SerializeField] private Vector3 init, final;

    public Vector3 getCurrentValue()
    {
        return Vector3.Lerp(init, final, GetCurrent());
    }

    public void setInit(Vector3 New)
    {
        init = New;
    }

    public Vector3 getFinal()
    {
        return final;
    }

    public float calculateSpeed(float time)
    {
        float distance = Vector3.Distance(init, final);
        return distance / time;
    }

    public void assignSpeedAsTime()
    {
        speed = calculateSpeed(time);
    }
}
