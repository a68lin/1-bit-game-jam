using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.backgroundColor = Color.white;
    }

    private void Update()
    {
        transform.position = target.position + offset;
    }
}
