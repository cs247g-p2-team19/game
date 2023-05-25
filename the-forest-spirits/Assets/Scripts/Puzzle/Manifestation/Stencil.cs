using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Stencil : AutoMonoBehaviour
{
    public bool IsOverlapping {
        get {
            Bounds? overlapping = CombinedBounds;
            if (!overlapping.HasValue) return false;

            return UpperLimit.Encapsulates2D(overlapping.Value) && !LowerLimit.Encapsulates2D(overlapping.Value);
        }
    }

    private Bounds? CombinedBounds {
        get {
            return targetWords.Aggregate(null, (Bounds? b, Word w) => {
                if (!b.HasValue) return w.Bounds;
                b.Value.Encapsulate(w.Bounds);
                return b;
            });
        }
    }
    
    private Bounds UpperLimit => new Bounds(collider.bounds.center, collider.bounds.size * 1.1f);
    private Bounds LowerLimit => new Bounds(collider.bounds.center, collider.bounds.size * 0.7f);

    private void LateUpdate() {
        switch (IsOverlapping) {
            case true when !_wasOverlapping:
                _wasOverlapping = true;
                onReachTarget.Invoke(this);
                break;
            case false when _wasOverlapping:
                _wasOverlapping = false;
                onLeaveTarget.Invoke(this);
                break;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0.4f, 0.7f, 0.4f);
        Gizmos.DrawWireCube(UpperLimit.center, UpperLimit.size);
        Gizmos.color = new Color(0.7f, 0.4f, 0.4f);
        Gizmos.DrawWireCube(LowerLimit.center, LowerLimit.size);
    }

    [Required]
    public Collider2D collider;

    public Word[] targetWords;

    public UnityEvent<Stencil> onReachTarget;
    public UnityEvent<Stencil> onLeaveTarget;

    private bool _wasOverlapping;
}