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
        //controls.Mouse.Position.performed += ctx => OnMouseHover();
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
    private void OnMouseHover() {
        DetectObject(0);
    }

    private void OnMouseClick() {
        DetectObject(1);
    }

    private void DetectObject(int caseSwitcher) {
        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider != null) {
                if (caseSwitcher == 0) {
                    IHoverable hoverable = hit.collider.GetComponent<IHoverable>();
                    if (hoverable != null) {
                        hoverable.OnHoverAction(hoverable);
                    }
                    Debug.Log("Hit: " + hit.collider.tag);
                }
                else if (caseSwitcher == 1) {
                    ISelectable selectable = hit.collider.GetComponent<ISelectable>();
                    if (selectable != null) {
                        selectable.OnClickAction(selectable);
                    }
                    Debug.Log("Hit: " + hit.collider.tag);
                }
                
            }

        }
    }
}
