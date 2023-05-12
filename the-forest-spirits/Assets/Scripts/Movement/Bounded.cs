using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Given a Boundary, bounds the given object
/// within it. Extend this class with
/// logic to get the left/right and up/down extents
/// so that the object always stays in bounds.
/// </summary>
public class Bounded : MonoBehaviour
{
    public Boundary by;

    // By default, no extents; object will just slide to the center before it stops.
    protected virtual float GetHorizontalExtent() => 0f;
    protected virtual float GetVerticalExtent() => 0f;

    private void LateUpdate() {
        float objHorizontalExtent = GetHorizontalExtent();
        float objVerticalExtent = GetVerticalExtent();

        var position = transform.position;
        position = new Vector3(
            Mathf.Clamp(position.x, by.Rect.xMin + objHorizontalExtent, by.Rect.xMax - objHorizontalExtent),
            Mathf.Clamp(position.y, by.Rect.yMin + objVerticalExtent, by.Rect.yMax - objVerticalExtent),
            position.z
        );
        transform.position = position;

    }
}
