using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Controls input;
    public static event Action<Vector2> moveChange;

    private void Awake()
    {
        input = new Controls();
    }

    private void OnEnable()
    {
        input.Player.Enable();

        input.Player.Move.performed += OnMovePerformed;
        input.Player.Move.canceled += OnMoveCancelled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveChange?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        moveChange?.Invoke(Vector2.zero);
    }
}
