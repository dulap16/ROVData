using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    public Handler handler;

    public GameObject suntProst;
    public GameObject symbolGroup;
    public GameObject symbolPrefab;

    /// Town bounds
    private List<float> list;
    private float xmin, xmax, ymin, ymax;

    private float width = 30;
    private float height = 30;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        list = handler.CalculateBoundsOfGroup();
        xmin = list[0]; xmax = list[1]; ymin = list[2]; ymax = list[3];
        symbolGroup.transform.position = new Vector2(0, 0);

        width = xmax - xmin;
        height = ymax - ymin;

        Debug.Log(suntProst.transform.GetChild(1).position.y);
        PoissonDiscSampler sampler = new PoissonDiscSampler(width, height, radius);
        foreach (Vector2 sample in sampler.Samples())
        {
            Vector3 position = new Vector3(sample.x, sample.y, 0);
            Instantiate(symbolPrefab, position, Quaternion.identity, symbolGroup.transform);
        }
        
        symbolGroup.transform.position = new Vector2(xmin, ymin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
