using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoverable {
    void OnHoverAction();

    // had to declare this guy here to make the previousHoverable logic inside the InputManager raycast method work the way I wanted
    // it was not enough to just cast Tile(previousHoverable)
    void UnhoverTile();
}