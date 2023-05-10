using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRenamer : MonoBehaviour
{
    // RENAMES IMPROPERLY NAMED OBJECTS

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            string Name = child.name;
            if (Name.Contains('.'))
                Name = Name.Substring(0, Name.Length - 4);
            Name = Name.ToLower();
            child.name = Name;
        }
    }
}
