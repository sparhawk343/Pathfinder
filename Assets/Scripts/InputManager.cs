using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {
    private Controls controls;
    private Camera mainCamera;
    private IHoverable previousHoverable;

    private void Awake() {
        controls = new Controls();
        mainCamera = Camera.main;
    }

    private void Start() {
        controls.Mouse.Click.performed += ctx => OnMouseClick();
        controls.Mouse.Position.performed += ctx => OnMouseHover();
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
        if (!EventSystem.current.IsPointerOverGameObject()) {
            DetectObject(1);
        }
    }


    // method for raycasting mouse interaction. I made a homebrew switch kind of statement to distinguish between click and hover
    // there is probably some better w
    private void DetectObject(int caseSwitcher) {
        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider != null) {
                if (caseSwitcher == 0) {

                    IHoverable hoverable = hit.collider.GetComponent<IHoverable>();
                    if (hoverable != null) {
                        if (hoverable != previousHoverable) {
                            if (previousHoverable != null) {
                                previousHoverable.UnhoverTile();
                            }
                            hoverable.OnHoverAction();
                            previousHoverable = hoverable;
                        }
                    }
                }
                else if (caseSwitcher == 1) {
                    ISelectable selectable = hit.collider.GetComponent<ISelectable>();
                    if (selectable != null) {
                        selectable.OnClickAction(selectable);
                    }
                }

            }

        }
    }
}
