using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tag : MonoBehaviour
{
    // ONLY USEFUL FOR THE CURSOR FOLLOWER

    public TextMeshProUGUI text;
    private Vector3 initialScale;
    private void Awake()
    {
        initialScale = transform.localScale;
    }

    public void ChangeText(string t)
    {
        text.text = t;
    }

    public string GetText()
    {
        return text.text;
    }

    public void MakeVisible()
    {
        transform.localScale = initialScale;
    }

    public void MakeInvisible()
    {
        transform.localScale = Vector3.zero;
    }
}
