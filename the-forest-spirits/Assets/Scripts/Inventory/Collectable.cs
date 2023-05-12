using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Defines an object that can be collected.
///
/// It should be given a name in the Unity editor.
/// </summary>
public class Collectable : MonoBehaviour
{
    public bool IsItem => Item != null;
    public InventoryItem Item { get; private set; } = null;

    public bool canBeCollected = true;
    public string itemName;

    [TextArea] public string itemDescription;

    public UnityEvent onCollect;
    
    private void Awake() {
        Item = GetComponent<InventoryItem>();
    }

    public void Touch() {
        onCollect.Invoke();
        Lil.Guy.onCollectAny.Invoke(this);
        if (IsItem) {
            Lil.Guy.PickUp(Item);
        }
    }

    public void SetCollectable(bool flag) {
        canBeCollected = flag;
    }
}