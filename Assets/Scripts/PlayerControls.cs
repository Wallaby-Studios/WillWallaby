using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction jumpAction;

    private void OnEnable() {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable() {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake() {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    public Vector2 GetMove() {
        return moveAction.ReadValue<Vector2>();
    }

    public bool GetJump() {
        return jumpAction.WasPressedThisFrame();
    }
}
