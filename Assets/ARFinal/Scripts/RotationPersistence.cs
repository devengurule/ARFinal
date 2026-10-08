using UnityEngine;

public class RotationPersistence : MonoBehaviour
{
    private Vector3 forwardVector;

    private Quaternion persistentRotation;

    private void Start()
    {
        Vector3 upVector = -Physics.gravity.normalized;

        Quaternion platformRotation = Quaternion.LookRotation(forwardVector, upVector);
    }

    private void Update()
    {
        ResetRotation();
    }

    private void ResetRotation()
    {
        Vector3 upVector = -Physics.gravity.normalized;

        Quaternion platformRotation = Quaternion.LookRotation(forwardVector, upVector);

        transform.rotation = platformRotation;
    }

    public void SetForwardVector(Vector3 vector)
    {
        forwardVector = vector;
    }
}