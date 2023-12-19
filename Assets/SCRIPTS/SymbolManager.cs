using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    public Handler handler;
    public DisplayModeHandler dmh;

    public GameObject judet;
    public GameObject symbolGroup;
    public GameObject individualSymbolGroup;
    public GameObject symbolPrefab;

    /// Town bounds
    private List<float> list;
    private float xmin, xmax, ymin, ymax;

    private float width = 30;
    private float height = 30;
    public float radius;

    public float maxRadius, minRadius;
    public AnimationCurve _curve;

    public bool generated = false;

    // Start is called before the first frame update
    public void Generate()
    {
        foreach (Transform child in symbolGroup.transform)
            Destroy(child.gameObject);

        generated = false;
        judet = handler.judetGO;

        list = handler.CalculateBoundsOfGroup();
        xmin = list[0]; xmax = list[1]; ymin = list[2]; ymax = list[3];
        symbolGroup.transform.position = new Vector3(xmin, ymin, 1f);

        width = xmax - xmin;
        height = ymax - ymin;

        PoissonDiscSampler sampler = new PoissonDiscSampler(width, height, radius);
        foreach (Vector2 sample in sampler.Samples())
        {
            Vector3 position = new Vector3(sample.x + xmin, sample.y + ymin, 0);
            Instantiate(symbolPrefab, position, Quaternion.identity, symbolGroup.transform);
        }

        generated = true;
    }

    public void GenerateForOneRegion(OverlappingRegion region)
    {
        float Radius = maxRadius - ((float)region.value / (float)handler.max) * (maxRadius - minRadius);

        judet = handler.judetGO;
        list = region.CalculateBoundsOfRegion();
        IndividualSymbolGroup sg = individualSymbolGroup.transform.Find(region.name).GetComponent<IndividualSymbolGroup>();

        var Xmin = list[0];
        var Xmax = list[1];
        var Ymin = list[2];
        var Ymax = list[3];

        sg.showingPosition = new Vector3(Xmin, Ymin, 1f);
        sg.Show();

        var Width = Xmax - Xmin;
        var Height = Ymax - Ymin;

        PoissonDiscSampler sampler = new PoissonDiscSampler(Width, Height, Radius);
        foreach (Vector2 sample in sampler.Samples())
        {
            Vector3 position = new Vector3(sample.x + Xmin, sample.y + Ymin, 0);
            Symbol symbol = Instantiate(symbolPrefab, position, Quaternion.identity, sg.transform).GetComponent<Symbol>();

            symbol.setTarget(region);
        }
    }

}
