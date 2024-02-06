using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ModelLaoder : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    // Use the Inspector to map image names to model prefabs
    public Dictionary<string, GameObject> imageToModelMap;

    void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var addedImage in args.added)
        {
            // Check if the recognized image name is in the map
            if (imageToModelMap.TryGetValue(addedImage.referenceImage.name, out GameObject modelPrefab))
            {
                // Instantiate the model at the image's position and rotation
                Instantiate(modelPrefab, addedImage.transform.position, addedImage.transform.rotation);
            }
        }
    }
}
