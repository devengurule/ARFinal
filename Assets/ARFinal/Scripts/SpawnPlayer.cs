using System;
using System.Collections;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject startUi;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPos;

    public static event Action PlayerSpawned;

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

    private void OnSpawnPlayer()
    {
        StartCoroutine(SpawnPlayerEnumerator());
    }

    private IEnumerator SpawnPlayerEnumerator()
    {
        yield return null;

        if (playerObject == null)
        {
            if(startUi.activeSelf) startUi.SetActive(false);
            PlayerSpawned?.Invoke();
            playerObject = Instantiate(playerPrefab, playerSpawnPos.position, Quaternion.identity, transform);
        }

        StopAllCoroutines();
    }
}
