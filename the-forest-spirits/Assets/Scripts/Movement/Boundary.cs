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


    private RectTransform _rect;

    private void Awake() {
        _rect = GetComponent<RectTransform>();
    }

    private void OnDrawGizmos() {
        var gizBounds = GetComponent<RectTransform>().GetWorldBounds();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gizBounds.center, gizBounds.size);
    }
}