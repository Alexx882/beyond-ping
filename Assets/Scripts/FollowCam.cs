using System;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform player;         // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 0, -10); // Default offset (adjust as needed)
    public float smoothSpeed = 0.125f; // Smoothing factor

    private void Start()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerGravityDrag>().enabled = true;
    }

    void LateUpdate()
    {
        // Calculate the desired position based on player's position + offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

        // Optionally, make the camera always look at the player
        transform.LookAt(player);
    }
}