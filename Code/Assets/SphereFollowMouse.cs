using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFollowMouse : MonoBehaviour
{
    public float followSpeed = 5.0f; // Adjust this to control the follow speed.

    private void Update()
    {
        // Get the current mouse position in screen coordinates.
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen coordinates to world coordinates.
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0.5f)); // Adjust the '10.0f' to your desired Z position.

        // Move the sphere towards the mouse position.
        transform.position = Vector3.Lerp(transform.position, mousePosition, Time.deltaTime * followSpeed);
    }
}