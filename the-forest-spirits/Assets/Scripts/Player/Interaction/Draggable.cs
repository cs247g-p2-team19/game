using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : AutoMonoBehaviour
{
    // Start is called before the first frame update

    [AutoDefaultMainCamera]
    public Camera mainCamera;

    private Vector3 _targetPos;
    private Vector2 _lastPos;
    private bool _dragging;

    public void OnDrag(Vector2 screenPos) {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(screenPos);

        if (!_dragging) {
            _targetPos = transform.position;
            _lastPos = mousePos;
            _dragging = true;
            return;
        }

        Vector2 moveVec = mousePos - _lastPos;
        _targetPos += (Vector3) moveVec;

        transform.position = _targetPos;

        _lastPos = mousePos;
    }

    public void DoneDragging() {
        _dragging = false;
    }
}