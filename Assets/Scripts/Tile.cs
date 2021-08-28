using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 position;
    public int gCost = 0;
    public int hCost = 0;
    public int fCost = 0;
    public List<Tile> neighbors = new List<Tile>();
    public bool isTraversible = true;
}
