using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxTorque;
    private Camera _camera;
    private Rigidbody rb;
    private Vector2 moveVector;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        _camera = Camera.main;

        InputHandler.moveChange += OnMoveChange;
        InputHandler.jump += OnJump;
    }

    private void OnBecameInvisible()
    {
        transform.position = startPos;
    }

    private void Update()
    {
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
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
