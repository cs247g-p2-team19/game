using UnityEngine;

/**
 * Extension of Bounded that makes sure
 * no part of the objects leaves the Boundary.
 */
[RequireComponent(typeof(Collider2D))]
public class Bounded2DObject : Bounded
{
    private Collider2D _collider;

    private void Awake() {
        _collider = GetComponent<Collider2D>();
    }

    protected override float GetHorizontalExtent() => _collider.bounds.extents.x;


    protected override float GetVerticalExtent() => _collider.bounds.extents.y;
}