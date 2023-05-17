using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    // MAPS PIXEL COLOR TO INTENSITY

    public Texture2D img;
    public Color[] array;

    // Start is called before the first frame update
    void Start()
    {
        /// GET COLORS
        array = new Color[img.width];

        for(int i = 0; i < img.width; i++)
        {
            Color pixel = img.GetPixel(i, 100);
            array[i] = pixel;
        }
    }

    public Color CalculateShade(int value, int max, float alpha)
    {
        Color c = array[array.Length - 1 - (int)(((float)value / (float)max) * array.Length)];
        c.a = alpha;
        return c;
    }

    public Color CalculateGrayscale(int value, int max, float alpha)
    {
        float percentage =  (1 - ((float)value / (float)max));
        return new Color(percentage, percentage, percentage, alpha);
    }
}
