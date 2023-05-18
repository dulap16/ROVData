using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    public GameObject suntProst;
    public GameObject symbolGroup;
    public GameObject symbolPrefab;

    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        PoissonDiscSampler sampler = new PoissonDiscSampler(100, 100, 1);
        foreach (Vector2 sample in sampler.Samples())
        {
            Instantiate(symbolPrefab, sample, Quaternion.identity, symbolGroup.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
