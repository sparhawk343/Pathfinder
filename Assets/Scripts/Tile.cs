using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, ISelectable
{
    [HideInInspector] public int gCost;
    [HideInInspector] public int hCost;
    [HideInInspector] public bool isTraversible = true;
    [HideInInspector] public Tile parentTile;

    [HideInInspector] public int gridX;
    [HideInInspector] public int gridY;
    [HideInInspector] public bool isSelected;


    public int fCost {
        get { return gCost + hCost; }
    }

    public void OnClickAction() {
        isSelected = !isSelected;

        if (isSelected == false) {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material.color = Color.green;
        }
        else {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material.color = Color.white;
        }
        
    }


}
