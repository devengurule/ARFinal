using UnityEngine;

public class BlobShadowScript : MonoBehaviour
{
    private static readonly int PlayerPositionID = Shader.PropertyToID("_PlayerPosition");

    private void LateUpdate()
    {
        Shader.SetGlobalVector(PlayerPositionID, transform.position);
    }

}
