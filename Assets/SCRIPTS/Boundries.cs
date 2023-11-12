using System;
using System.Collections;
using UnityEngine;

namespace Assets.SCRIPTS
{
    [Serializable]
    public class Boundries
    {
        [SerializeField] public float north, east, south, west;

        public bool isThisWithinBoundries(Boundries container)
        {
            if (north < container.north && south > container.south && east < container.east && west > container.west)
                return true;

            return false;
        }
    }
}