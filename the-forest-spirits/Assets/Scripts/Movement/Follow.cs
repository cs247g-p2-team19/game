using UnityEngine;
using UnityEngine.Serialization;

/**
 * Smoothly follow another GameObject
 */
public class Follow : MonoBehaviour
{
    [Tooltip("The transform this will be anchored to")]
    public Transform anchor;
    
    [Tooltip("If specified, we will make sure that we don't move outside of the boundaries specified.")]
    public Bounded respectBoundaries;

    [Tooltip("How 'loose' we are. The lower this number, the faster we snap.")]
    [FormerlySerializedAs("tension")]
    [Range(0.01f, 0.3f)]
    public float looseness = 0.1f;

    [Tooltip("The maximum speed at which we'll snap")]
    public float maxSpeed = 100;

    [Header("Direction locking")]
    [Tooltip("Don't follow on the X axis")]
    public bool lockX = false;
    [Tooltip("Don't follow on the Y axis")]
    public bool lockY = false;
    [Tooltip("Don't follow on the Z axis")]
    public bool lockZ = true;

    // Used for SmoothDamp
    private Vector3 _velocity;
    
    // Needed because checking != null on GameObjects is slow (i.e. shouldn't happen in Update).
    private bool _isrespectBoundariesNotNull;

    private void OnEnable() {
        _isrespectBoundariesNotNull = respectBoundaries != null;
        if (respectBoundaries != null) {
            respectBoundaries.StartConsuming();
        }
    }

    private void OnDisable() {
        if (respectBoundaries != null) {
            respectBoundaries.StopConsuming();
        }
    }

    void Update() {
        Vector3 target;
        if (_isrespectBoundariesNotNull && respectBoundaries.enabled) {
            target = respectBoundaries.ClampToBoundary(anchor.position);
        }
        else {
            target = anchor.position;
        }

        Vector3 next = Vector3.SmoothDamp(transform.position, target, ref _velocity, looseness, maxSpeed,
            Time.deltaTime);

        if (lockX) next.x = transform.position.x;
        if (lockY) next.y = transform.position.y;
        if (lockZ) next.z = transform.position.z;

        transform.position = next;
    }
}