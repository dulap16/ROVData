using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemFixer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
    }
}
