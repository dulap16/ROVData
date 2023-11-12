using Assets.SCRIPTS;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KeepingTheMapWithinTheBorders : MonoBehaviour
{
    private BoxCollider2D _coll;

    public BoundriesGetter map;
    public Boundries initBoundries;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = map.transform.position;

        _coll = this.GetComponent<BoxCollider2D>();

        initBoundries = map.getBoundries();
    }

    public void mapChangedScale()
    {
        map.boundriesUpdated();

        Boundries currentBoundries = map.getBoundries();
        
        Vector2 newScale = new Vector2(currentBoundries.north - initBoundries.north, currentBoundries.east - initBoundries.east);
    }
}
