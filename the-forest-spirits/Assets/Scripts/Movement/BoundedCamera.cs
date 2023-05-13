using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extension of Bounded that makes sure no
/// part of the camera leaves the Boundary.
/// </summary>
[RequireComponent(typeof(Camera))]
public class BoundedCamera : Bounded
{
    private Camera _camera;

    private void Awake() {
        _camera = GetComponent<Camera>();
    }

    private Vector2 Extents
    {
        get {
            float z = by.Bounds.center.z;
            Vector3 min = _camera.ViewportToWorldPoint(new Vector3(0, 0, z));
            Vector3 max = _camera.ViewportToWorldPoint(new Vector3(1, 1, z));

            return new Vector2((max.x - min.x) / 2, (max.y - min.y) / 2);
        }
    }

    protected override float GetHorizontalExtent() {
        if (_camera.orthographic) {
            return _camera.orthographicSize * Screen.width / Screen.height;
        }

        return Extents.x;
    }

    protected override float GetVerticalExtent() {
        if (_camera.orthographic) {
            return _camera.orthographicSize;
        }

        return Extents.y;
    }
}