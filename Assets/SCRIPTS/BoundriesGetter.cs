using Assets.SCRIPTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundriesGetter : MonoBehaviour
{
    public BoxCollider2D coll;
    [SerializeField] private Boundries boundries;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();

        boundriesUpdated();
    }

    void OnDrawGizmosSelected()
    {
        coll = GetComponent<BoxCollider2D>();
        var b = coll.bounds;

        boundries = new Boundries();
        boundries.north = b.center.y + b.size.y;
        boundries.south = b.center.y - b.size.y;
        boundries.east = b.center.x + b.size.x;
        boundries.west = b.center.x - b.size.x;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(b.center + new Vector3(b.size.x, b.size.y, b.size.z) * 0.5f, 0.1f);
        Gizmos.DrawSphere(b.center + new Vector3(-b.size.x, b.size.y, b.size.z) * 0.5f, 0.1f);
        Gizmos.DrawSphere(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f, 0.1f);
        Gizmos.DrawSphere(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f, 0.1f);
    }

    public void boundriesUpdated()
    {
        coll = GetComponent<BoxCollider2D>();
        if (boundries == null)
            boundries = new Boundries();

        var b = coll.bounds;
        Debug.Log(b.size.y);

        boundries.north = b.center.y + b.size.y;
        boundries.south = b.center.y - b.size.y;
        boundries.east = b.center.x + b.size.x;
        boundries.west = b.center.x - b.size.x;
    }

    public Boundries getBoundries()
    {
        boundriesUpdated();

        return boundries;
    }
}
