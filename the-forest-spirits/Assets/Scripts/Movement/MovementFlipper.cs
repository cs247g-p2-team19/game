using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Facing
{
    Left = -1,
    Right = 1
}

public class MovementFlipper : MonoBehaviour
{
    public Facing facing = Facing.Right; 
    
    private Coroutine _currentFlip;

    private Vector3 _lastPosition;

    private float _initialScaleX;

    private void Start() {
        _lastPosition = transform.position;
        _initialScaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update() {
        Vector3 currentPosition = transform.position;
        Vector3 movement = currentPosition - _lastPosition;
        _lastPosition = currentPosition;

        if (movement.x < 0 && facing == Facing.Right) {
            facing = Facing.Left;
            Flip();
        } else if (movement.x > 0 && facing == Facing.Left) {
            facing = Facing.Right;
            Flip();
        }
    }

    private void Flip() {
        if (_currentFlip != null) {
            StopCoroutine(_currentFlip);
            _currentFlip = null;
        }
        
        _currentFlip = this.AutoLerp(transform.localScale.x, ((int) facing) * _initialScaleX, 0.2f, Utility.EaseInOutF, localScaleX => {
            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        });
        this.WaitThen(_currentFlip, () => _currentFlip = null);
    }
}
