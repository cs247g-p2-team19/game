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

    public int Consumers { get; set; } = 0;

    public bool boundX = true;
    public bool boundY = false;


    // By default, no extents; object will just slide to the center before it stops.
    protected virtual float GetHorizontalExtent() => 0f;
    protected virtual float GetVerticalExtent() => 0f;

    private void LateUpdate() {
        if (Consumers > 0) return;
        
        transform.position = ClampToBoundary(transform.position);
    }

    public Vector3 ClampToBoundary(Vector3 location) {
        float objHorizontalExtent = GetHorizontalExtent();
        float objVerticalExtent = GetVerticalExtent();

        float xMin = by.Bounds.min.x + objHorizontalExtent;
        float xMax = by.Bounds.max.x - objHorizontalExtent;
        float yMin = by.Bounds.min.y + objVerticalExtent;
        float yMax = by.Bounds.max.y - objVerticalExtent;

        var position = location;
        float targetX;
        if (boundX) {
            // If the boundary is smaller than the extents, choose the center.
            targetX = xMax < xMin ? (xMin + xMax) / 2 : Mathf.Clamp(position.x, xMin, xMax);
        }
        else {
            targetX = position.x;
        }

        float targetY;
        if (boundY) {
            targetY = yMax < yMin ? (yMin + yMax) / 2 : Mathf.Clamp(position.y, yMin, yMax);
        }
        else {
            targetY = position.y;
        }

        return new Vector3(targetX, targetY, position.z);
    }
}