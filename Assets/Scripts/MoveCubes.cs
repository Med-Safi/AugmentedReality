using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubes : MonoBehaviour
{
    public static GameObject lastPlacedCube = null; // Tracks the last placed cube
    public float moveDistance = 0.1f; // How far the cube moves with each press

    // Call these methods from the buttons' OnClick() events
    public void MoveLeft() => MoveCube(Vector3.left);
    public void MoveRight() => MoveCube(Vector3.right);
    public void MoveForward() => MoveCube(Vector3.forward);
    public void MoveBack() => MoveCube(Vector3.back);

    private void MoveCube(Vector3 direction)
    {
        if (lastPlacedCube != null)
        {
            lastPlacedCube.transform.position += direction * moveDistance;
        }
    }

    public void DoubleSpeed()
    {
        moveDistance *= 2; // Doubles the movement distance
    }
}
