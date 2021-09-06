using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector] public int gCost;
    [HideInInspector] public int hCost;
    [HideInInspector] public bool isTraversible = true;
    [HideInInspector] public Tile parentTile;

    [HideInInspector] public int gridX;
    [HideInInspector] public int gridY;


    public int fCost {
        get { return gCost + hCost; }
    }


}
