using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour, ISelectable
{
    [HideInInspector] public int gCost;
    [HideInInspector] public int hCost;
    [HideInInspector] public bool isTraversible = true;
    [HideInInspector] public Tile parentTile;

    [HideInInspector] public int gridX;
    [HideInInspector] public int gridY;
    [HideInInspector] public bool isSelected;

    public UnityEvent<ISelectable> OnSelectedEvent;


    public int fCost {
        get { return gCost + hCost; }
    }

    public void OnClickAction(ISelectable selectable) {

        if (isSelected == false) {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material.color = Color.green;
            isSelected = true;
        }
        else {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            Color color;
            if (ColorUtility.TryParseHtmlString("#B9AAAA", out color)) {
                meshRenderer.material.color = color;
                isSelected = false;
            }
        }

        OnSelectedEvent.Invoke(selectable);
    }


}
