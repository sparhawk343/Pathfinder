using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public Vector2 position;
    [SerializeField] public int gCost;
    [SerializeField] public int hCost;
    [SerializeField] public List<Tile> neighbors = new List<Tile>();
    [SerializeField] public bool isTraversible = true;
    [SerializeField] public Tile parentTile;

    [SerializeField] public int gridX;
    [SerializeField] public int gridY;

    public Tile(int _gridX, int _gridY) {
        _gridX = gridX;
        _gridY = gridY;
    }

    public int fCost {
        get { return gCost + hCost; }
    }
}
