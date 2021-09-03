using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    List<Tile> open = new List<Tile>();
    List<Tile> closed = new List<Tile>();

    Tile startTile;
    Tile targetTile;
    Tile currentTile;

    const int baseDiagonalCost = 14;
    const int baseStraightCost = 10;

    public TileGrid grid;

    private void Awake() {
        grid = GetComponent<TileGrid>();
    }


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
                RetracePath(startTile, targetTile);
                return;
            }

            // go through all neighbor tiles that the current tile has, and check if they are traversible
            foreach (Tile neighborTile in grid.GetNeighbors(currentTile)) {
                if (!neighborTile.isTraversible || closed.Contains(neighborTile)) {
                    continue;
                }

                int newMovementCostToNeighbor = currentTile.gCost + GetDistance(currentTile, neighborTile);
                if (newMovementCostToNeighbor < neighborTile.gCost || !open.Contains(neighborTile)) {
                    neighborTile.gCost = newMovementCostToNeighbor;
                    neighborTile.hCost = GetDistance(neighborTile, targetTile);
                    neighborTile.parentTile = currentTile;
                    if (!open.Contains(neighborTile)) {
                        open.Add(neighborTile);
                    }
                }
            }
        }
    }

    void RetracePath(Tile startTile, Tile endTile) {
        List<Tile> path = new List<Tile>();
        Tile currentTile = targetTile;

        while (currentTile != startTile) {
            path.Add(currentTile);
            currentTile = currentTile.parentTile;
        }
        path.Reverse();
    }

    int GetDistance(Tile tileA, Tile tileB) {
        int distanceX = Mathf.Abs(tileA.gridX - tileB.gridX);
        int distanceY = Mathf.Abs(tileA.gridY - tileB.gridY);

        if (distanceX > distanceY) {
            return baseDiagonalCost * distanceY + baseStraightCost * (distanceX - distanceY);
        }
        else {
            return baseDiagonalCost * distanceX + baseStraightCost * (distanceY - distanceX);
        }

    }


    // TODO:
    // finish mirroring MainGrid into TileGrid
    // get rid of Node
    // make a test for pathfinding
    // implement mouse events (hover, select) -> check notes from David
    //      -> inputmanager, player, tile
    //      -> interfaces for ISelectable, IHoverable
    //      -> input using new input system (callbacks)
    // implement player
    // implement action points
    // implement movement (walk/dash)
    // implement path line
    // implement vfx/highlights/gizmos for tile
}
