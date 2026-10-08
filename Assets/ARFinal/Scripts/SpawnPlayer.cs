using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPos;

    private GameObject playerObject;

    private void OnEnable()
    {
        TouchInputHandler.SpawnPlayer += OnSpawnPlayer;
        PlayerMovement.RespawnPlayer += OnSpawnPlayer;
    }
    private void OnDisable()
    {
        TouchInputHandler.SpawnPlayer -= OnSpawnPlayer;
        PlayerMovement.RespawnPlayer -= OnSpawnPlayer;
    }

    private void Update()
    {
        //Debug.Log(transform.eulerAngles);
    }

    private void OnSpawnPlayer()
    {
        if(playerObject == null)
        {
            playerObject = Instantiate(playerPrefab, playerSpawnPos.position, Quaternion.identity, transform);
        }
    }
}
