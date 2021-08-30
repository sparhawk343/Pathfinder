using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    List<Tile> open = new List<Tile>();
    List<Tile> closed = new List<Tile>();
    List<Tile> currentPath = new List<Tile>();
    List<Tile> newPath = new List<Tile>();

    Tile startTile = new Tile();
    Tile targetTile = new Tile();
    Tile currentTile = new Tile();


    private void FindPath(Tile start, Tile target) {

        open.Add(startTile);

        // set currentTile to the tile in the open list with the lowest fCost
        while (currentTile != targetTile) {
            open.Sort((tileA, tileB) => {
                if (tileA.fCost <= tileB.fCost) {
                    return 1;
                }
                else {
                    return -1;
                }
            });
            currentTile = open.FirstOrDefault();
            // this null check is to prevent running out of tiles when trying to set an inaccesible
            // tile as target (should probably prevent this from happening in some other way later)
            if (currentTile == null) {
                Debug.Log("Tried to access inaccessible tile");
                return;
            }
            // remove currentTile from open and add it to closed
            open.Remove(currentTile);
            closed.Add(currentTile);

            // if currentTile is same as target, we have a path
            if (currentTile == targetTile) {
                return;
            }

            // go through all neighbor tiles that the current tile has, and check if they are traversible
            foreach (Tile neighbor in currentTile.neighbors) {
                if (!neighbor.isTraversible || closed.Contains(neighbor)) {
                    continue;
                }

                // if new path is shorter implement!!
                if (newPath.Count < currentPath.Count || !open.Contains(neighbor)) {
                    neighbor.fCost = neighbor.gCost + neighbor.hCost;
                    // set parent of neighbor to currentTile
                    if (!open.Contains(neighbor)) {
                        open.Add(neighbor);
                    }
                }
            }







        }
    }
    
}
