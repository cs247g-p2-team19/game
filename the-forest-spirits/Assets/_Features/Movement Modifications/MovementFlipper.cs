using UnityEngine;

public enum Facing
{
    Left,
    Right
}

public enum FlipMode
{
    Instant,
    Rotate
}

/**
 * Automatically flips this sprite around Paper Mario-style when it changes directions.
 */
public class MovementFlipper : AutoMonoBehaviour
{
    [Tooltip("The way this sprite is initially facing")]
    public Facing initialFacing = Facing.Right;

    public FlipMode flipMode = FlipMode.Rotate;

    [Tooltip("How long a flip takes to finish")]
    public float flipTime = 0.4f;

    private Coroutine _currentFlip;

    private Vector3 _lastPosition;

    private Quaternion _initialRotation;
    private Facing _facing;

    private void Start() {
        _lastPosition = transform.position;
        _initialRotation = transform.rotation;
        _facing = initialFacing;
    }

    void Update() {
        Vector3 currentPosition = transform.position;
        Vector3 movement = currentPosition - _lastPosition;
        _lastPosition = currentPosition;

        switch (movement.x) {
            case < 0 when _facing == Facing.Right:
                _facing = Facing.Left;
                Flip();
                break;
            case > 0 when _facing == Facing.Left:
                _facing = Facing.Right;
                Flip();
                break;
        }
    }

    private void Flip() {
        if (_currentFlip != null) {
            StopCoroutine(_currentFlip);
            _currentFlip = null;
        }

        Quaternion target = Quaternion.Euler(_initialRotation.x,
            _initialRotation.y + (_facing == initialFacing ? 0 : 180), _initialRotation.y);

        if (flipMode == FlipMode.Rotate) {
            _currentFlip = this.AutoLerp(transform.rotation, target, flipTime,
                Utility.EaseInOut<Quaternion>(Quaternion.Lerp),
                rotation => { transform.rotation = rotation; });
            this.WaitThen(_currentFlip, () => _currentFlip = null);
        }
        else {
            transform.rotation = target;
        }
    }
}