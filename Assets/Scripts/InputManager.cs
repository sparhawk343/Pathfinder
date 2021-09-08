using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Controls controls;
    private Camera mainCamera;

    private void Awake() {
        controls = new Controls();
        mainCamera = Camera.main;
    }

    private void Start() {
        controls.Mouse.Click.performed += ctx => OnMouseClick();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }

    private void OnMouseClick() {
        DetectObject();
    }

    private void DetectObject() {
        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider != null) {
                ISelectable selectable = hit.collider.GetComponent<ISelectable>();
                if (selectable != null) {
                    selectable.OnClickAction(selectable);
                }
                Debug.Log("Hit: " + hit.collider.tag);
            }

        }
    }
}
