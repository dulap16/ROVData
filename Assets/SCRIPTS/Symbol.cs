using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    public OverlappingRegion owner;
    public List<Collider> colliders;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("spawned");
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
        colliders = Physics.OverlapSphere(transform.position, 0.5f).ToList<Collider>();

        foreach(Collider coll in colliders)
        {
            try
            {
                coll.gameObject.GetComponent<OverlappingRegion>();
            } catch(System.Exception e)
            {
                // colliders.Remove(coll);
            }
        }

        if (colliders.Count == 0)
            Destroy(this.gameObject);
    }
}
