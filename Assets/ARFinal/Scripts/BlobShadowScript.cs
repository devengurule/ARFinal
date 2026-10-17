using UnityEngine;

public class BlobShadowScript : MonoBehaviour
{
    private static readonly int PlayerPositionID = Shader.PropertyToID("_PlayerPosition");
    private static readonly int PlayerSpawnedID = Shader.PropertyToID("_isPlayerSpawned");

    private void Start()
    {
        Shader.SetGlobalFloat(PlayerSpawnedID, 1f);
    }

    private void LateUpdate()
    {
        Shader.SetGlobalVector(PlayerPositionID, transform.position);
    }

}
