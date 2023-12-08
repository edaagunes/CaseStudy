using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 camOffset;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 followPosition = player.position + camOffset;
            transform.position = Vector3.Lerp(transform.position, followPosition, 0.1f);
        }
    }
}
