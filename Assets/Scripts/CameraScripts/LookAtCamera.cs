using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        // This will make the object's forward direction point towards the camera's position
        // and its up direction might change based on the camera's orientation
        
        transform.LookAt(cam.transform);
    }
}
