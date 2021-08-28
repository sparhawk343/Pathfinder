using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    List<Tile> open = new List<Tile>();
    List<Tile> closed = new List<Tile>();
    List<Tile> nonTraversible = new List<Tile>();

    Tile startTile = new Tile();
    Tile targetTile = new Tile();
    Tile currentTile = new Tile();

    private void Start() {
        open.Add(startTile);
    }


    private void FindPath(Tile start, Tile target) {
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
            // tile as target (probably prevent this from happening in some other way later)
            if (currentTile == null) {
                Debug.Log("Tried to access inaccessible tile");
                return;
            }
            // remove currentTile from open and add it to closed
            open.Remove(currentTile);
            closed.Add(currentTile);

            // if currentTile is same as target, we have a path
            if (currentTile == targetTile) {
                // return path here
            }

            // go through all neighbor tiles that the current tile has, and check if they are traversible
            foreach (Tile neighbor in currentTile.neighbors) {
                if (nonTraversible.Contains(neighbor) || closed.Contains(neighbor)) {
                    continue;
                }
            }

            // keep watching https://www.youtube.com/watch?v=-L-WgKMFuhE&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=1
            // for a* tips





        }
    }
    
}
