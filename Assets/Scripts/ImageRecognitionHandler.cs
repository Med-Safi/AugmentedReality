using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;

public class ImageRecognitionHandler : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public MoveCubes moveCubesScript;
    public string imageUrl = "https://mix-ar.ru/content/ios/marker.jpg ";
    public Renderer targetRenderer;

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
            StartCoroutine(DownloadImage(imageUrl));
        }
    }
    IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            targetRenderer.material.mainTexture = texture;
        }
        else
        {
            Debug.LogError("Error downloading image: " + request.error);
        }
    }
}
