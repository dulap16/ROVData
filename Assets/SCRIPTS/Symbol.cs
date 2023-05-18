using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    public OverlappingRegion owner;
    public List<Collider2D> colliders;


    // RAYCASTING
    /*
    public ContactFilter2D contactFilter;
    public List<string> namesOfHits;
    [SerializeField] RaycastHit2D[] hits = new RaycastHit2D[5];

    
    */

    // another try
    private Ray ray;
    public LayerMask layersToHit;
    public float maxDistance = 3;

    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray(transform.position, transform.forward);

        CheckCollision();

        if (colliders.Count == 0) ;
        // Destroy(this.gameObject);
        else
        {
            owner = colliders[0].GetComponent<OverlappingRegion>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        try
        {
            OverlappingRegion region = collision.gameObject.GetComponent<OverlappingRegion>();
            Debug.Log(region.name);
        } catch(System.Exception e)
        {
            Debug.Log("Collided with something else");
        }
    }

    private void CheckCollision()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layersToHit))
        {
            OverlappingRegion region = hit.transform.gameObject.GetComponent<OverlappingRegion>();
            owner = region;
            region.AddSymbol(this);
        }
        else Destroy(this.gameObject);
    }


}
