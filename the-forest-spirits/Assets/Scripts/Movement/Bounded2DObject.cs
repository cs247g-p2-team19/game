using UnityEngine;

/**
 * Extension of Bounded that makes sure
 * no part of the objects leaves the Boundary.
 */
[RequireComponent(typeof(Collider2D))]
public class Bounded2DObject : Bounded
{
    private Collider2D _collider {
        get {
            if (_wasCached) return _cachedCollider;
            _wasCached = true;
            _cachedCollider = GetComponent<Collider2D>();
            return _cachedCollider;
        }
    }

    private Collider2D _cachedCollider;
    private bool _wasCached;

    protected override float GetHorizontalExtent() => _collider.bounds.extents.x;


    protected override float GetVerticalExtent() => _collider.bounds.extents.y;
}