using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour {

    private List<Tile> open = new List<Tile>();
    private List<Tile> closed = new List<Tile>();

    private Tile currentTile;

    private const int baseDiagonalCost = 14;
    private const int baseStraightCost = 10;

    public TileGrid grid;

    public List<Tile> FindPath(Tile start, Tile target) {

        currentTile = null;
        open.Add(start);

        // set currentTile to the tile in the open list with the lowest fCost
        while (currentTile != target) {
            open.Sort((tileA, tileB) => {
                if (tileA.fCost <= tileB.fCost) {
                    return 1;
                }
                else {
                    return -1;
                }
            });
            currentTile = open.LastOrDefault();

            // this null check is to prevent running out of tiles and getting stuck in an infinite while loop
            // when trying to set an inaccesible tile as target
            // (should probably prevent this from happening in some other way later)
            if (currentTile == null) {
                Debug.Log("Tried to access inaccessible tile");
                return null;
            }
            // remove currentTile from open and add it to closed
            open.Remove(currentTile);
            closed.Add(currentTile);

            // if currentTile is same as target, we have a path
            if (currentTile == target) {
                break;
            }

            // go through all neighbor tiles that the current tile has, and check if they are traversible
            foreach (Tile neighborTile in grid.GetTraversibleNeighbors(currentTile)) {
                if (closed.Contains(neighborTile)) {
                    open.Remove(neighborTile);
                    continue;
                }

                int newMovementCostToNeighbor = currentTile.gCost + GetDistance(currentTile, neighborTile);
                if (newMovementCostToNeighbor < neighborTile.gCost || !open.Contains(neighborTile)) {
                    neighborTile.gCost = newMovementCostToNeighbor;
                    neighborTile.hCost = GetDistance(neighborTile, target);
                    neighborTile.parentTile = currentTile;
                    if (!open.Contains(neighborTile)) {
                        open.Add(neighborTile);
                    }
                }
            }
        }
        // here is where we return the path
        // (outside the while loop, which has the same check as the path found check, basically)
        return RetracePath(start, target);
    }

    // method that retraces where we went, flips the path and returns it so that it can be drawn
    // this is where we make use of the setting of a parent tile from the cost calculation
    private List<Tile> RetracePath(Tile startTile, Tile endTile) {
        List<Tile> path = new List<Tile>();
        Tile currentTile = endTile;

        while (currentTile != startTile) {
            path.Add(currentTile);
            currentTile = currentTile.parentTile;
        }
        path.Reverse();
        foreach (Tile tile in path) {
            tile.SetIsPath();
        }
        return path;
    }

    // method to get absolute distance between start and end tiles
    private int GetDistance(Tile tileA, Tile tileB) {
        int distanceX = Mathf.Abs(tileA.gridX - tileB.gridX);
        int distanceY = Mathf.Abs(tileA.gridY - tileB.gridY);

        if (distanceX > distanceY) {
            return baseDiagonalCost * distanceY + baseStraightCost * (distanceX - distanceY);
        }
        else {
            return baseDiagonalCost * distanceX + baseStraightCost * (distanceY - distanceX);
        }

    }

    // this used to be a test method. it owes its continued existence to the count check, that makes sure we can't trigger the path logic with only
    // one tile selected
    public void FindSpecificPath() {
        if (grid.selectedTiles.Count == 2) {
            FindPath(grid.selectedTiles[0], grid.selectedTiles[1]);
        }
    }

    // cleanup method for reset button
    public void ResetPathFinder() {
        open.Clear();
        closed.Clear();
        grid.ResetGrid();
    }


    // Stretch goals:
    // optimize algorithm with heap
    // implement path line
    // implement player
    // implement action points
    // implement movement (walk/dash)
    // implement vfx highlights for tile or animations for juiciness (instead of / on top of color change)
    // implement vfx stuff for path line
}
