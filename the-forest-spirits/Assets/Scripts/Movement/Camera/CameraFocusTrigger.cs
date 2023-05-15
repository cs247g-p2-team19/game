using UnityEngine;

public enum CameraFocusTriggerType
{
    FocusWhileWithin,
    FocusOnEnter,
    FocusOnLeave,
    UnfocusOnEnter,
    UnfocusOnLeave,
    None
}

/**
 * Can be attached to anything with a trigger-mode Collider2D
 * to cause the camera to snap to a given [CameraFocusArea].
 * Can also be invoked manually when the [triggerType] is None.
 */
public class CameraFocusTrigger : MonoBehaviour
{
    #region Unity-exposed public fields

    public CameraFocusTriggerType triggerType = CameraFocusTriggerType.FocusWhileWithin;

    [Tooltip("The area to focus on")]
    public CameraFocusArea area;

    [Tooltip("The camera follower to snap")]
    public Follow cameraFollow;

    [Range(0.01f, 1f)]
    public float resizeTime = 0.5f;

    #endregion

    private Bounded _bounds;
    private Camera _camera;

    // Used to restore the camera to its original position
    private Transform _oldFollower;
    private float _originalOrthoSize;
    private float _targetSize;
    private Vector3 _originalOffset;

    // Used for moving the camera's orthographic size using SmoothDamp
    private Coroutine _resizeCoroutine;

    // Smoothing function used for transitioning the orthographic size
    private static readonly Utility.LerpFn<float> _lerp = Utility.EaseOut<float>(Mathf.SmoothStep);

    #region Unity Events

    private void Awake() {
        _bounds = cameraFollow.GetComponent<Bounded>();
        _camera = cameraFollow.GetComponent<Camera>();
        _originalOrthoSize = _camera.orthographicSize;
        _targetSize = _originalOrthoSize;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>() == null) return;

        switch (triggerType) {
            case CameraFocusTriggerType.FocusOnEnter:
            case CameraFocusTriggerType.FocusWhileWithin:
                Focus();
                break;
            case CameraFocusTriggerType.UnfocusOnEnter:
                Unfocus();
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Player>() == null) return;
        switch (triggerType) {
            case CameraFocusTriggerType.FocusOnLeave:
                Focus();
                break;
            case CameraFocusTriggerType.FocusWhileWithin:
            case CameraFocusTriggerType.UnfocusOnLeave:
                Unfocus();
                break;
        }
    }

    #endregion

    #region Public Methods

    public void Focus() {
        if (_bounds != null) {
            _bounds.enabled = false;
        }

        if (_resizeCoroutine != null) {
            StopCoroutine(_resizeCoroutine);
        }

        _oldFollower = cameraFollow.anchor;
        _originalOffset = cameraFollow.offset;
        cameraFollow.anchor = area.transform;
        cameraFollow.offset = Vector3.zero;

        float areaAspect = area.Bounds.size.x / area.Bounds.size.y;
        float cameraAspect = 1f * _camera.aspect;

        // Need to match camera width to area width
        if (areaAspect > cameraAspect) {
            _targetSize = area.Bounds.extents.x / cameraAspect;
        }

        // need to match camera height to area height
        if (areaAspect <= cameraAspect) {
            _targetSize = area.Bounds.extents.y;
        }

        _resizeCoroutine = this.AutoLerp(_camera.orthographicSize, _targetSize, resizeTime, _lerp,
            size => _camera.orthographicSize = size);
    }

    public void Unfocus() {
        if (_bounds != null) {
            _bounds.enabled = true;
        }

        if (_resizeCoroutine != null) {
            StopCoroutine(_resizeCoroutine);
        }


        if (_oldFollower == null) return;

        cameraFollow.anchor = _oldFollower;
        cameraFollow.offset = _originalOffset;

        _targetSize = _originalOrthoSize;
        _resizeCoroutine = this.AutoLerp(_camera.orthographicSize, _targetSize, resizeTime, _lerp,
            size => _camera.orthographicSize = size);
    }

    #endregion
}