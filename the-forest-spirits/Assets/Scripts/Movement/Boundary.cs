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
    // how many times can i use the word rect in one file
    public Rect Rect => _rect.rect;

    private RectTransform _rect;
    private void Awake() {
        _rect = GetComponent<RectTransform>();
    }
}
