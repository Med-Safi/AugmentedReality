using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognitionHandler : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public MoveCubes moveCubesScript; // Reference to the MoveCubes script

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            // Assuming you want to double the speed for any recognized image
            moveCubesScript.DoubleSpeed();
        }
    }
}
