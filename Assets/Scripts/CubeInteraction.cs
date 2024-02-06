using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CubeInteraction : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject cubePrefab;
    private float timeMouseDown;
    private bool interactedWithCube = false; // Flag to track interaction with cubes

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timeMouseDown = Time.time;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            interactedWithCube = false; // Reset flag at the start of a click

            // Check if we hit a cube
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Cube"))
                {
                    // Flag that we've interacted with a cube
                    interactedWithCube = true;

                    // If it's a short click, change color
                    // (Deletion will be handled on mouse up if it's a long press)
                    hit.transform.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            float duration = Time.time - timeMouseDown;

            // Long press logic for cube deletion
            if (duration > 0.5f && interactedWithCube)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.CompareTag("Cube"))
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
            else if (!interactedWithCube) // Place a new cube if there was no interaction with existing cubes
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(Input.mousePosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    Instantiate(cubePrefab, hitPose.position, hitPose.rotation);
                }
            }
        }
    }
}
