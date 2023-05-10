using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateReader : MonoBehaviour
{
    // MAPS COORDINATE AND NAME TO REGIONS ON THE MAP

    public GameObject prefab;
    private float imageX, imageY;
    private string[] list;

    public class Coord
    {
        public float index, x, y;
        public float mapx, mapy;
        public string name;
    }

    public class CoordList
    {
        public Coord[] list;
    }

    private string[] namesLines;
    private string[] coordsLines;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Coordinate Reader Active");

        coordsLines = System.IO.File.ReadAllLines(@"C:\Users\tudor\OneDrive\Documents\Unity\TESTARE 2\Assets\SCRIPTS\coords2.txt");
        namesLines = System.IO.File.ReadAllLines(@"C:\Users\tudor\OneDrive\Documents\Unity\TESTARE 2\Assets\SCRIPTS\listaLoc.txt");


        /// READ COORDS 
        CoordList cl = new CoordList();
        cl.list = new Coord[60];

        for (int i = 0; i < coordsLines.Length; i++)
        {
            // Debug.Log(lines[i]);
            list = coordsLines[i].Split(' ');

            if (i == 0)
            {
                imageX = Int32.Parse(list[0]);
                imageY = Int32.Parse(list[1]);
            }
            else
            {
                Coord coord = new Coord();
                coord.index = Int32.Parse(list[0]);
                coord.x = Int32.Parse(list[1]);
                coord.y = Int32.Parse(list[2]);
                coord.mapx = coord.x / imageX - 0.5f;
                coord.mapy = coord.y / imageY - 0.5f;

                GameObject newObject = (GameObject)Instantiate(prefab, new Vector3(coord.mapx, coord.mapy, -3000), Quaternion.identity, GameObject.Find("Canvas").transform.Find("Map"));
                newObject.transform.localScale = new Vector3(0.01f, 0.01f);
                // newObject.transform.localPosition = new Vector3(coord.mapx, coord.mapy * -1, -101);

                cl.list[i] = coord;
                Debug.Log(coord.mapx + " " + coord.mapy);
            }
        }
    }
}
