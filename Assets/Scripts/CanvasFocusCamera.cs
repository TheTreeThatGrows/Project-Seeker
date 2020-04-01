using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFocusCamera : MonoBehaviour
{

    public Camera FocusCamera;

    void Update()
    {
        transform.LookAt(transform.position + FocusCamera.transform.rotation * Vector3.forward,
            FocusCamera.transform.rotation * Vector3.up);
    }
}
