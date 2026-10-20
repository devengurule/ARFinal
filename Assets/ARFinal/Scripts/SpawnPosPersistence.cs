using UnityEngine;

public class SpawnPosPersistence : MonoBehaviour
{
    public Vector3 startPosition { get; set; }

    private void LateUpdate()
    {
        transform.position = startPosition;
    }
}
