using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector2 _screenPos;
    private Vector2 _lastPos;
    private bool _dragging;
    void Start() {
        _lastPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (_dragging) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(_screenPos);
            Vector2 moveVec = mousePos - _lastPos;
            var position = transform.position;
            transform.position = new Vector3(position.x + moveVec.x, position.y + moveVec.y,
                position.z);
            //Debug.Log(mousePos);
            _lastPos = mousePos;
        }
    }

    public void SetScreenPos(Vector2 screenPos) {
        _screenPos = screenPos;
    }

    public void SetDragging(bool setDrag) {
        _dragging = setDrag;
    }
}
