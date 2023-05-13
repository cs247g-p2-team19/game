using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Facing
{
    Left,
    Right
}

public class MovementFlipper : MonoBehaviour
{
    public Facing initialFacing = Facing.Right;

    private Coroutine _currentFlip;

    private Vector3 _lastPosition;

    private Quaternion _initialRotation;
    private Facing _facing;

    private void Start() {
        _lastPosition = transform.position;
        _initialRotation = transform.rotation;
        _facing = initialFacing;
    }

    // Update is called once per frame
    void Update() {
        Vector3 currentPosition = transform.position;
        Vector3 movement = currentPosition - _lastPosition;
        _lastPosition = currentPosition;

        if (movement.x < 0 && _facing == Facing.Right) {
            _facing = Facing.Left;
            Flip();
        }
        else if (movement.x > 0 && _facing == Facing.Left) {
            _facing = Facing.Right;
            Flip();
        }
    }

    private void Flip() {
        if (_currentFlip != null) {
            StopCoroutine(_currentFlip);
            _currentFlip = null;
        }

        Quaternion target = Quaternion.Euler(_initialRotation.x,
            _initialRotation.y + (_facing == initialFacing ? 0 : 180), _initialRotation.y);

        _currentFlip = this.AutoLerp(transform.rotation, target, 0.4f, Utility.EaseInOut<Quaternion>(Quaternion.Lerp),
            rotation => { transform.rotation = rotation; });
        this.WaitThen(_currentFlip, () => _currentFlip = null);
    }
}