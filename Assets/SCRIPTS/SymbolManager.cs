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
    public GameObject symbolPrefab;

    /// Town bounds
    private List<float> list;
    private float xmin, xmax, ymin, ymax;

    private float width = 30;
    private float height = 30;
    public float radius;

    public bool generated = false;

    IEnumerator waiter(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);

        handler.DelegateSymbols();
    }

    // Start is called before the first frame update
    public void Generate()
    {
        judet = handler.judetGO;

        list = handler.CalculateBoundsOfGroup();
        xmin = list[0]; xmax = list[1]; ymin = list[2]; ymax = list[3];
        symbolGroup.transform.position = new Vector3(xmin, ymin, 1f);

        width = xmax - xmin;
        height = ymax - ymin;

        Debug.Log(judet.transform.GetChild(1).position.y);
        PoissonDiscSampler sampler = new PoissonDiscSampler(width, height, radius);
        foreach (Vector2 sample in sampler.Samples())
        {
            Vector3 position = new Vector3(sample.x + xmin, sample.y + ymin, 0);
            Instantiate(symbolPrefab, position, Quaternion.identity, symbolGroup.transform);
        }

        generated = true;
    }

    /*
    public void JudetChanged()
    {
        judet = handler.judetGO;

        list = handler.CalculateBoundsOfGroup();
        xmin = list[0]; xmax = list[1]; ymin = list[2]; ymax = list[3];
        symbolGroup.transform.position = new Vector3(xmin, ymin, 1f);

        width = xmax - xmin;
        height = ymax - ymin;

        Debug.Log(judet.transform.GetChild(1).position.y);
        PoissonDiscSampler sampler = new PoissonDiscSampler(width, height, radius);
        foreach (Vector2 sample in sampler.Samples())
        {
            Vector3 position = new Vector3(sample.x + xmin, sample.y + ymin, 0);
            Instantiate(symbolPrefab, position, Quaternion.identity, symbolGroup.transform);
        }
    }
    */
}
