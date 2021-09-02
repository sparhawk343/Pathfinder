using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {
    Tile[,] grid;

    int gridSizeX;
    int gridSizeY;

    void PopulateGrid() {
        grid = new Tile[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++){
                grid[x, y] = new Tile(x, y);
                Tile tile = new Tile(x, y);
                Instantiate<Tile>(tile);
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

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbors.Add(new Tile(checkX, checkY));
                }
            }
        }
        return neighbors;
    }

}
