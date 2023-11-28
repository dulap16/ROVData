using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TextureToggle : MonoBehaviour
{
    public SpriteRenderer texture;
    private bool isTextureVisible = true;

    // Start is called before the first frame update
    void Start()
    {
        texture = GameObject.Find("Full Texture").GetComponent<SpriteRenderer>();
        makeTextureVisible();
    }

    public void toggleVisibility()
    {
        if (isTextureVisible)
            makeTextureInvisible();
        else makeTextureVisible();
    }

    public void makeTextureVisible()
    {
        isTextureVisible = true;
        texture.color = new Color(255, 255, 255, 255);
    }

    public void makeTextureInvisible()
    {
        isTextureVisible = false;
        texture.color = new Color(255, 255, 255, 0);
    }

    public void changeTexture(SpriteRenderer newTexture)
    {
        texture = newTexture;
    }
}
