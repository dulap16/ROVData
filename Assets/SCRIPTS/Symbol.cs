using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    public OverlappingRegion owner;

    private Ray ray;
    public LayerMask layersToHit;
    public float maxDistance = 3;

    public bool isInSelection;
    public string regionHit = "";

    private new Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        ray = new Ray(transform.position, transform.forward);

        CheckCollision();
    }


    private void CheckCollision()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layersToHit))
        {
            OverlappingRegion region = hit.transform.gameObject.GetComponent<OverlappingRegion>();
            owner = region;
            region.AddSymbol(this);
            regionHit = hit.transform.name;
        }
        else Destroy(this.gameObject);
    }

    public void Show()
    {
        Color c = renderer.material.color;
        c.a = 1;
        renderer.material.color = c;
    }

    public void Hide()
    {
        Color c = renderer.material.color;
        c.a = 0;
        renderer.material.color = c;
    }
}
