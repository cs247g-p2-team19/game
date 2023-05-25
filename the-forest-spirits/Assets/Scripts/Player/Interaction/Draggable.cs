using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : AutoMonoBehaviour
{
    // Start is called before the first frame update

    [AutoDefaultMainCamera]
    public Camera mainCamera;

    private Vector2 _lastPos;
    private bool _dragging;

    public void OnDrag(Vector2 screenPos) {
        if (!_dragging) {
            _lastPos = mainCamera.ScreenToWorldPoint(screenPos);
            _dragging = true;
        }

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(screenPos);
        Vector2 moveVec = mousePos - _lastPos;
        var position = transform.position;
        transform.position = position + (Vector3) moveVec;

        _lastPos = mousePos;
    }

    public void DoneDragging() {
        _dragging = false;
    }
}