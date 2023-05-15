using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Defines an area used by [CameraFocusTrigger] to focus the
 * camera on something. Will show up as a blue rectangle in
 * the editor. Does nothing by itself.
 */
[RequireComponent(typeof(RectTransform))]
public class CameraFocusArea : MonoBehaviour
{
    public Bounds Bounds => _rect.GetWorldBounds();


    private RectTransform _rect;

    private void Awake() {
        _rect = GetComponent<RectTransform>();
    }

    private void OnDrawGizmos() {
        var gizBounds = GetComponent<RectTransform>().GetWorldBounds();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(gizBounds.center, gizBounds.size);
    }
}