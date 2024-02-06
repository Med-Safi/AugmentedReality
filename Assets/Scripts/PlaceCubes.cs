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

            Vector2 fakeTouchPosition = Input.mousePosition;
            if (!IsPointerOverUIObject(fakeTouchPosition))
            {
                Ray ray = Camera.main.ScreenPointToRay(fakeTouchPosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.CompareTag("Cube"))
                    {
                        selectedCube = hit.transform.gameObject;
                        selectedCube.GetComponent<Renderer>().material.color = Random.ColorHSV();

                        return; 
                    }
                }
                TryPlaceCube(fakeTouchPosition);
            }
        }
        else if (Input.GetMouseButtonUp(0) && selectedCube != null && Time.time - timeMouseDown > 0.5f)
        {
            
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
            MoveCubes.lastPlacedCube = newCube; 
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
        controlArrowsUI.SetActive(cubeCount > 0);
    }
}

