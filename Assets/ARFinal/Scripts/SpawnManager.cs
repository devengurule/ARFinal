using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager arImageManager;
    [SerializeField] private List<GameObject> platforms = new();

    private Dictionary<string, GameObject> platformDictionary = new();
    private GameObject spawnObject;

    private void Start()
    {
        foreach(var platform in platforms)
        {
            platformDictionary.Add(platform.gameObject.name, platform);
        }
    }


    private void OnEnable() => arImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
    private void OnDisable() => arImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (ARTrackedImage image in args.added)
        {
            SpawnPlatforms(image);
        }
    }

    private void SpawnPlatforms(ARTrackedImage trackedImage)
    {
        string imageID = trackedImage.referenceImage.name;

        if(platformDictionary.TryGetValue(imageID, out GameObject platform))
        {
            spawnObject = Instantiate(platform, trackedImage.transform.position, Quaternion.identity, trackedImage.transform);
        }
    }
}
