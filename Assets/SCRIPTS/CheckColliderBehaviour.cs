using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColliderBehaviour : MonoBehaviour
{
    // SCRIPT FOR WHEN HAVING MULTIPLE OBJECTS THAT NEED COLLIDER BEHAVIOUR CHECKED

    public bool clicked = false;
    public bool over = false;

    private void OnMouseOver()
    {
        over = true;
    }

    private void OnMouseExit()
    {
        over = false;
    }

    private void OnMouseDown()
    {
        clicked = true;
        Debug.Log("slider");
    }

    private void OnMouseUp()
    {
        clicked = false;
    }
}
