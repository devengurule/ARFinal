using UnityEngine;

public class RotationPersistence : MonoBehaviour
{
    private Quaternion persistentRotation;

    private void Start()
    {
        persistentRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = persistentRotation;
    }
}