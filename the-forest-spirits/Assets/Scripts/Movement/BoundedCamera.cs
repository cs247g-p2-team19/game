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

    protected override float GetHorizontalExtent() => _camera.orthographicSize * Screen.width / Screen.height;

    protected override float GetVerticalExtent() => _camera.orthographicSize;
}