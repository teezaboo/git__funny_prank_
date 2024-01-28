using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    public float scrollSpeed = 5f; // ปรับความเร็วการเลื่อนตามความต้องการ
    public float minX = -10f; // ขอบเขตต่ำสุดในแกน X
    public float maxX = 10f; // ขอบเขตสูงสุดในแกน X
    public float minY = -10f; // ขอบเขตต่ำสุดในแกน Y
    public float maxY = 10f; // ขอบเขตสูงสุดในแกน Y

    void Update()
    {
        EdgeScrolling();
    }

    void EdgeScrolling()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Get the screen width and height
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Set a threshold for edge scrolling (e.g., 10% of the screen width or height)
        float edgeThreshold = 0.1f;

        // Check if the mouse is near the edges for scrolling
        if (mousePosition.x < edgeThreshold * screenWidth && transform.position.x > minX)
        {
            // Scroll left
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
        else if (mousePosition.x > (1 - edgeThreshold) * screenWidth && transform.position.x < maxX)
        {
            // Scroll right
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }

        if (mousePosition.y < edgeThreshold * screenHeight && transform.position.y > minY)
        {
            // Scroll down
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }
        else if (mousePosition.y > (1 - edgeThreshold) * screenHeight && transform.position.y < maxY)
        {
            // Scroll up
            transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        }

        // Clamp the camera position to stay within the specified bounds
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
