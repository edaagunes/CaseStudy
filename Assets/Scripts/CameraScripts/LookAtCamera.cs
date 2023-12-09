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
        transform.LookAt(cam.transform);
    }
}
