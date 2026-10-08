using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Controls input;
    public static event Action<Vector2> moveChange;
    public static event Action jump;

    private void Awake()
    {
        input = new Controls();
    }

    private void OnEnable()
    {
        input.Player.Enable();

        input.Player.Move.performed += OnMovePerformed;
        input.Player.Move.canceled += OnMoveCancelled;

        input.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveChange?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        moveChange?.Invoke(Vector2.zero);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jump?.Invoke();
    }
}
