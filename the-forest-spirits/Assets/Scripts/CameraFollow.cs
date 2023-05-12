using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform anchor;

    [Range(0.01f, 0.3f)] public float tension = 0.1f;
    public float maxSpeed = 100;

    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

    private Vector3 _velocity;

    // Update is called once per frame
    void Update() {
        Vector3 next = Vector3.SmoothDamp(transform.position, anchor.position, ref _velocity, tension, maxSpeed,
            Time.deltaTime);

        if (lockX) next.x = transform.position.x;
        if (lockY) next.y = transform.position.y;
        if (lockZ) next.z = transform.position.z;

        transform.position = next;
    }
}