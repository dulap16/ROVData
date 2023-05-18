using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    public GameObject suntProst;
    public GameObject symbolGroup;
    public GameObject symbolPrefab;

    private float width = 30;
    private float height = 30;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        PoissonDiscSampler sampler = new PoissonDiscSampler(30, 30, 0.3f);
        foreach (Vector2 sample in sampler.Samples())
        {
            Vector2 position = new Vector2(sample.x - width / 2, sample.y - height / 2);
            Instantiate(symbolPrefab, position, Quaternion.identity, symbolGroup.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
