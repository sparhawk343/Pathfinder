using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 position;
    public int gCost;
    public int hCost;
    public List<Tile> neighbors = new List<Tile>();
    public bool isTraversible = true;
    public Tile parentTile;

    public int gridX;
    public int gridY;

    public Tile(int _gridX, int _gridY) {
        _gridX = gridX;
        _gridY = gridY;
    }

    public int fCost {
        get { return gCost + hCost; }
    }
}
