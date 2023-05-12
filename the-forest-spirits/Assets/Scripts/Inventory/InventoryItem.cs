using System;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

/// <summary>
/// Defines an item that can be found in the inventory.
/// </summary>

[RequireComponent(typeof(Collectable))]
public class InventoryItem : MonoBehaviour
{
    public Collectable Collectable { get; private set; }
    
    public bool isDroppable;
    public bool isConsumable;
    public UnityEvent<InventoryItem> onCollect;
    public UnityEvent<InventoryItem> onUse;
    public UnityEvent<InventoryItem> onDrop;

    private void Awake() {
        Collectable = GetComponent<Collectable>();
    }
}