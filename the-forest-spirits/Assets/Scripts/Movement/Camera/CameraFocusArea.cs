using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
