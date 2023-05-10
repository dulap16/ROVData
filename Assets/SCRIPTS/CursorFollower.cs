using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    // TAG THAT FOLLOWS CURSOR AND APPEARS WHEN OVER A REGION

    private Vector3 pos;
    public float speed = 1f;
    public bool shown;
    public Tag tag;

    // Start is called before the first frame update
    void Start()
    {
        MakeInvisible();
    }

    // Update is called once per frame
    void Update()
    {
        pos = Input.mousePosition;
        pos.x = pos.x - 10;
        pos.y = pos.y + 20;
        pos.z = speed;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    public void ChangeText(string text)
    {
        tag.ChangeText(text);
    }

    public void MakeVisible()
    {
        shown = true;
        tag.MakeVisible();
    }

    public void MakeInvisible()
    {
        shown = false;
        tag.MakeInvisible();
    }

    public string GetText()
    {
        return tag.text.text;
    }
}
