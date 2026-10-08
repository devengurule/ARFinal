using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxTorque;
    private Camera _camera;
    private Rigidbody rb;
    private Vector2 moveVector;
    public static event Action RespawnPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _camera = Camera.main;

        InputHandler.moveChange += OnMoveChange;
        InputHandler.jump += OnJump;
    }

    private void Update()
    {
        if (rb == null) return;
        
        Debug.Log(transform.position.y);

        if (transform.position.y < 0)
        {
            RespawnPlayer?.Invoke();
        }

        Vector3 forward = _camera.transform.forward.normalized;
        Vector3 right = _camera.transform.right.normalized;

        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDirection = forward * moveVector.y + right * moveVector.x;

        moveDirection.Normalize();

        Vector3 torqueDirection = Vector3.Cross(Vector3.up, moveDirection);

        rb.AddTorque(torqueDirection * Mathf.Min(moveSpeed, maxTorque) * Time.deltaTime, ForceMode.Force);
    }

    private void OnMoveChange(Vector2 moveVector)
    {
        this.moveVector = moveVector;
    }

    private void OnJump()
    {
        if (rb == null) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
