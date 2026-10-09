using UnityEngine;

public class RotationPersistence : MonoBehaviour
{
    private Vector3 forwardVector;

    private Quaternion persistentRotation;

    private void Start()
    {
        Vector3 upVector = -Physics.gravity.normalized;

        Quaternion platformRotation = Quaternion.LookRotation(forwardVector, upVector);

        persistentRotation = platformRotation;
    }

    private void LateUpdate()
    {
        ResetRotation();
    }

    private void ResetRotation()
    {
        transform.rotation = persistentRotation;
    }

    public void SetForwardVector(Vector3 vector)
    {
        forwardVector = vector;
    }
}