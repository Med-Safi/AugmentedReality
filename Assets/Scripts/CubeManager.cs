using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public static CubeManager Instance; // Singleton
    private int cubeCount = 0;

    public GameObject controlUI; // Assign in inspector

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddCube()
    {
        cubeCount++;
        UpdateUIVisibility();
    }

    public void RemoveCube(GameObject cube)
    {
        cubeCount--;
        UpdateUIVisibility();
    }

    void UpdateUIVisibility()
    {
        if (cubeCount <= 0)
        {
            controlUI.SetActive(false);
        }
        else
        {
            controlUI.SetActive(true);
        }
    }
}
