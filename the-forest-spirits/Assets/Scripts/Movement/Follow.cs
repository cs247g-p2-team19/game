using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Smoothly follow another GameObject
/// </summary>
public class Follow : MonoBehaviour
{
    public Transform anchor;
    public Bounded respectBoundaries;

    [Range(0.01f, 0.3f)] public float tension = 0.1f;
    public float maxSpeed = 100;

    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = true;

    private Vector3 _velocity;
    private bool _isrespectBoundariesNotNull;

    private void OnEnable() {
        _isrespectBoundariesNotNull = respectBoundaries != null;
        if (respectBoundaries != null) {
            respectBoundaries.Consumers++;
        }
    }

    private void OnDisable() {
        if (respectBoundaries != null) {
            respectBoundaries.Consumers--;
        }
    }

    void Update() {
        Vector3 target;
        if (_isrespectBoundariesNotNull && respectBoundaries.enabled) {
            target = respectBoundaries.ClampToBoundary(anchor.position);
        }
        else {
            target = anchor.position;
        }
        
        Vector3 next = Vector3.SmoothDamp(transform.position, target, ref _velocity, tension, maxSpeed,
            Time.deltaTime);

        if (lockX) next.x = transform.position.x;
        if (lockY) next.y = transform.position.y;
        if (lockZ) next.z = transform.position.z;

        transform.position = next;
    }
}