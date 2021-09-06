using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {
    Tile[,] grid;
    public Transform tilePrefab;
    Vector2Int gridSize = new Vector2Int(10,10);

    float outlinePercent = 0.05f;

    public LayerMask nonTraversibleMask;
    //public Vector2 gridWorldSize;
    public float tileRadius = 1f;
    public float tileDiameter;




    private void Start() {
        tileDiameter = tileRadius * 2;
        PopulateGrid();
    }

    void PopulateGrid() {
        grid = new Tile[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++){
                
                Vector3 tilePosition = new Vector3(-gridSize.x / 2 + 0.5f + x, 0, -gridSize.y / 2 + 0.5f + y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90));
                newTile.parent = gameObject.GetComponent<TileGrid>().transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);

                bool traversible = (!Physics.CheckSphere(newTile.position, tileRadius, nonTraversibleMask));
                if (!traversible) {
                    //Debug.Log("this tile was not traversible");
                }

                grid[x, y] = newTile.GetComponent<Tile>();
                grid[x, y].gridX = Mathf.RoundToInt(tilePosition.x);
                grid[x, y].gridY = Mathf.RoundToInt(tilePosition.z);
                grid[x, y].isTraversible = traversible;
                //Debug.Log(grid[x, y].gridX + " " + grid[x, y].gridY);
            }
        }
    }

    public List<Tile> GetNeighbors(Tile tile) {
        List<Tile> neighbors = new List<Tile>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) {
                    continue;
                }
                int checkX = tile.gridX + x;
                int checkY = tile.gridY + y;

                if (checkX >= 0 && checkX < gridSize.x && checkY >= 0 && checkY < gridSize.y) {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbors;
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 0.5f, gridSize.y));

        if (grid != null) {

            //for (int x = 0; x < gridSize.x; x++) {
            //    for (int y = 0; y < gridSize.y; y++) {
            //        Gizmos.color = (grid[x, y].isTraversible) ? Color.white : Color.red;

            //        Gizmos.DrawCube(grid[x, y].transform.position, Vector3.one * (tileDiameter - 0.1f));
            //    }
            //}

            foreach (Tile tile in grid) {
                Gizmos.color = (tile.isTraversible) ? Color.white : Color.red;

                Gizmos.DrawCube(tile.transform.position, Vector3.one * (tileDiameter - 0.3f));
            }

        }
    }
}
