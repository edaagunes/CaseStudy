using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 camOffset;

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the position for the camera to follow based on player's position and offset
            
            Vector3 followPosition = player.position + camOffset;
            transform.position = Vector3.Lerp(transform.position, followPosition, 0.1f);
            
            // The third parameter (0.1f) controls the speed of the camera movement
            // Smaller values make it slower, larger values make it faster
        }
    }
}
