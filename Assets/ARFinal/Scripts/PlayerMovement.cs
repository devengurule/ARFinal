using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float distanceTillRespawn;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float staticDamping;
    [SerializeField] private float kinematicDamping;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxTorque;
    [SerializeField] private Vector2 jumpAngleRange;

    private Camera _camera;
    private Rigidbody rb;
    private Vector2 moveVector;
    public bool canJump { get; set; }
    public static event Action RespawnPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        InputHandler.moveChange += OnMoveChange;
        JumpButton.OnJump += OnJump;
        CollisionManager.CollidingWithGround += CheckForJump;
        CollisionManager.StoppedCollidingWithGround += ResetJump;
    }
    private void OnDisable()
    {
        InputHandler.moveChange -= OnMoveChange;
        JumpButton.OnJump -= OnJump;
        CollisionManager.CollidingWithGround -= CheckForJump;
        CollisionManager.StoppedCollidingWithGround -= ResetJump;
    }

    private void Update()
    {
        CheckForRespawn();

        Movement();
    }

    private void OnMoveChange(Vector2 moveVector)
    {
        this.moveVector = moveVector;
    }

    private void OnJump()
    {
        if (rb == null || !canJump) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = false;
    }

    private void CheckForJump(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 surfaceNormal = collision.contacts[0].normal;

            float finalAngle = FindAngleBetweenVectors(surfaceNormal, Vector3.up);

            if (finalAngle >= jumpAngleRange.x && finalAngle <= jumpAngleRange.y)
            {
                canJump = true;
                return;
            }
        }
    }

    private void CheckForRespawn()
    {
        if (Mathf.Abs(transform.position.y - _camera.transform.position.y) > distanceTillRespawn)
        {
            RespawnPlayer?.Invoke();
            Destroy(gameObject);
        }
    }

    private void Movement()
    {
        if (rb == null) return;

        if (moveVector == Vector2.zero) rb.angularDamping = staticDamping;
        else rb.angularDamping = kinematicDamping;

        Vector3 forward = _camera.transform.forward.normalized;
        Vector3 right = _camera.transform.right.normalized;

        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDirection = forward * moveVector.y + right * moveVector.x;

        moveDirection.Normalize();

        Vector3 torqueDirection = Vector3.Cross(Vector3.up, moveDirection);

        rb.AddTorque(torqueDirection * Mathf.Min(moveSpeed, maxTorque) * Time.deltaTime, ForceMode.Force);
    }

    private float FindAngleBetweenVectors(Vector3 a, Vector3 b)
    {
        float dotProduct = Vector3.Dot(a, b);
        float magnitude = a.magnitude * b.magnitude;
        float x = Mathf.Clamp(dotProduct / magnitude, 0f, 1f);

        float finalAngle = Mathf.Abs(Mathf.Acos(x) * Mathf.Rad2Deg);

        return finalAngle;
    }
}
