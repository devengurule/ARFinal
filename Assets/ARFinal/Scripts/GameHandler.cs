using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject jumpButton;
    [SerializeField] private GameObject restartButton;

    private static readonly int PlayerSpawnedID = Shader.PropertyToID("_isPlayerSpawned");

    private void OnEnable()
    {
        Shader.SetGlobalFloat(PlayerSpawnedID, 0f);

        jumpButton.SetActive(false);

        SpawnPlayer.PlayerSpawned += OnPlayerSpawned;
        CollisionManager.CollidedWIthEnd += GameEnd;
    }

    private void OnDisable()
    {
        SpawnPlayer.PlayerSpawned -= OnPlayerSpawned;
        CollisionManager.CollidedWIthEnd -= GameEnd;
    }

    private void OnPlayerSpawned()
    {
        if (!jumpButton.activeSelf) jumpButton.SetActive(true);
    }

    private void GameEnd()
    {
        if (jumpButton.activeSelf) jumpButton.SetActive(false);
        if (!restartButton.activeSelf) restartButton.SetActive(true);
    }

    public void Restart()
    {
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
        SceneManager.LoadScene(0);
    }
}
