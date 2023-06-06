using System;
using UnityEngine;
using UnityEngine.Events;

/**
 * Defines a component with a trigger-mode Collider2D
 * that the player may interact with.
 */
[RequireComponent(typeof(Collider2D))]
public class Tripwire : AutoMonoBehaviour
{
    public UnityEvent<Tripwire> onPlayerEnter;

    public void OnCross() {
        onPlayerEnter.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Player p = other.GetComponent<Player>();
        if (p == null) return;
        OnCross();
    }
}
