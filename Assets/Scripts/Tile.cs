using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour, ISelectable, IHoverable {
    [HideInInspector] public int gCost;
    [HideInInspector] public int hCost;
    [HideInInspector] public bool isTraversible = true;
    [HideInInspector] public Tile parentTile;

    [HideInInspector] public int gridX;
    [HideInInspector] public int gridY;

    [HideInInspector] public bool isSelected;
    [HideInInspector] public bool isHovered;
    [HideInInspector] public bool isInRange;
    [HideInInspector] public bool isPath;

    public UnityEvent<ISelectable> OnSelectedEvent;
    public UnityEvent<ISelectable> OnDeselectedEvent;
    private MeshRenderer meshRenderer;

    public Color isSelectedColor;
    public Color isHoveredColor;
    public Color isInRangeColor;
    public Color isPathColor;
    public Color defaultColor;


    public int fCost {
        get { return gCost + hCost; }
    }

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void OnClickAction(ISelectable selectable) {

        if (isSelected == false) {
            OnSelectedEvent.Invoke(selectable);
        }
        else {
            OnDeselectedEvent.Invoke(selectable);
        }

    }

    public void OnHoverAction() {
        isHovered = !isHovered;
        ChangeTileColor();
    }

    // this method not only changes colors but helps us keep track of tile state - I made this behavior to get around selection problems
    // when I started to run into having to reset/undo two steps back in the color history. having a priority order like this is better
    public void ChangeTileColor() {
        if (isSelected) {
            meshRenderer.material.color = isSelectedColor;
            return;
        }
        if (isHovered) {
            meshRenderer.material.color = isHoveredColor;
            return;
        }
        if (isPath) {
            meshRenderer.material.color = isPathColor;
            return;
        }
        if (isInRange) {
            meshRenderer.material.color = isInRangeColor;
            return;
        }
        meshRenderer.material.color = defaultColor;
    }

    public void UnhoverTile() {
        isHovered = false;
        ChangeTileColor();
    }

    public void DeselectTile() {
        isSelected = false;
        ChangeTileColor();
    }

    public void SelectTile() {
        isSelected = true;
        ChangeTileColor();
    }

    public void SetInRange() {
        isInRange = true;
        ChangeTileColor();
    }

    public void SetIsPath() {
        isPath = true;
        ChangeTileColor();
    }

    public void ResetTile() {
        isHovered = false;
        isSelected = false;
        isInRange = false;
        isPath = false;
        ChangeTileColor();
    }

}
