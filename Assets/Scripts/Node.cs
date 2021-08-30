using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node { 

    public bool traversible;
    public Vector3 worldPosition;

    public Node(bool _traversible, Vector3 _worldPosition) {
        traversible = _traversible;
        worldPosition = _worldPosition;
    }

}