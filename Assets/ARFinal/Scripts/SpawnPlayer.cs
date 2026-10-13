using System.Collections;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPos;

    private GameObject playerObject;

    private void OnEnable()
    {
        playerUI.SetActive(false);
        TouchInputHandler.SpawnPlayer += OnSpawnPlayer;
        PlayerMovement.RespawnPlayer += OnSpawnPlayer;
    }
    private void OnDisable()
    {
        TouchInputHandler.SpawnPlayer -= OnSpawnPlayer;
        PlayerMovement.RespawnPlayer -= OnSpawnPlayer;
    }

    private void OnSpawnPlayer()
    {
        StartCoroutine(SpawnPlayerEnumerator());
    }

    private IEnumerator SpawnPlayerEnumerator()
    {
        yield return null;

        if (playerObject == null)
        {
            if(!playerUI.activeSelf) playerUI.SetActive(true);
            playerObject = Instantiate(playerPrefab, playerSpawnPos.position, Quaternion.identity, transform);
        }

        StopAllCoroutines();
    }
}
