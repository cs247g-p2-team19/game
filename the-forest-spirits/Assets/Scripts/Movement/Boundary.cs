using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines a boundary that Bounded objects must stay within.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class Boundary : MonoBehaviour
{
    public Bounds Bounds => _rect.GetWorldBounds();


    private RectTransform _rect {
        get {
            if (_wasCached) return _cachedRect;
            
            _wasCached = true;
            _cachedRect = GetComponent<RectTransform>();

            return _cachedRect;
        }
    }

    private bool _wasCached = false;
    private RectTransform _cachedRect;

    private void OnDrawGizmos() {
        var gizBounds = _rect.GetWorldBounds();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gizBounds.center, gizBounds.size);
    }
}