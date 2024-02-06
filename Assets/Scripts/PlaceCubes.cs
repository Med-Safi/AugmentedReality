using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceCubes : MonoBehaviour
{

    public ARRaycastManager raycastManager;

    public GameObject cubePrefab;
    public GameObject controlArrowsUI;
    private GameObject selectedCube = null;

    private float timeMouseDown;
    private int cubeCount = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timeMouseDown = Time.time;

            // Convert mouse position to a fake touch position
            Vector2 fakeTouchPosition = Input.mousePosition;
            if (!IsPointerOverUIObject(fakeTouchPosition))
            {
                Ray ray = Camera.main.ScreenPointToRay(fakeTouchPosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.CompareTag("Cube"))
                    {
                        // Handle cube interaction
                        selectedCube = hit.transform.gameObject;
                        selectedCube.GetComponent<Renderer>().material.color = Random.ColorHSV();

                        return; // Prevent placing a new cube
                    }
                }
                // No cube was hit, try placing a new cube
                TryPlaceCube(fakeTouchPosition);
            }
        }
        else if (Input.GetMouseButtonUp(0) && selectedCube != null && Time.time - timeMouseDown > 0.5f)
        {
            // Long press, remove the cube
            Destroy(selectedCube);
            cubeCount--;
            selectedCube = null;
            UpdateControlArrowsVisibility();
        }
    }

    void TryPlaceCube(Vector2 position)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            cubeCount++;
            Pose hitPose = hits[0].pose;
            GameObject newCube = Instantiate(cubePrefab, hitPose.position, hitPose.rotation);
            MoveCubes.lastPlacedCube = newCube; // Assuming you have logic for MoveCubes
            UpdateControlArrowsVisibility();
        }
    }

    bool IsPointerOverUIObject(Vector2 pos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void UpdateControlArrowsVisibility()
    {
        // Control arrows are shown if there's at least one cube and hidden if there are none
        controlArrowsUI.SetActive(cubeCount > 0);
    }
    /*
    public ARRaycastManager raycastManager;
    public GameObject cubePrefab;

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!IsPointerOverUIObject(touch))
            {
                TryPlaceCube(touch.position);
            }
        }


        // Check for mouse input (for editor testing)
        else if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to a fake touch position
            Vector2 fakeTouchPosition = Input.mousePosition;

            // Check if click is over a UI element
            if (!IsPointerOverUIObject(fakeTouchPosition))
            {
                TryPlaceCube(fakeTouchPosition);
            }
        }
    }

    void TryPlaceCube(Vector2 position)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            // Instantiate a new cube at the hit position and rotation, then store the reference in newCube
            GameObject newCube = Instantiate(cubePrefab, hitPose.position, hitPose.rotation);
            MoveCubes.lastPlacedCube = newCube; // Update the reference to the last placed cube

            // Debug log for testing
            Debug.Log("Cube placed at: " + hitPose.position);
        }
    }
    bool IsPointerOverUIObject(Touch touch)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    bool IsPointerOverUIObject(Vector2 pos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    */
}

