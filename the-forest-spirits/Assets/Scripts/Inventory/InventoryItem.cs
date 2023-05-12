using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

/// <summary>
/// Defines an item that can be found in the inventory.
/// </summary>

[RequireComponent(typeof(Collectable))]
public class InventoryItem : MonoBehaviour
{
    private static Dictionary<string, InventoryItem> _items = new();

    public static InventoryItem GetItemById(string id) {
        return _items.TryGetValue(id, out var item) ? item : null;
    }

    public Collectable Collectable { get; private set; }

    public string itemId;
    
    public bool isDroppable;
    public bool isConsumable;
    public UnityEvent<InventoryItem> onCollect;
    public UnityEvent<InventoryItem> onUse;
    public UnityEvent<InventoryItem> onDrop;

    private void Awake() {
        Collectable = GetComponent<Collectable>();
        Debug.Assert(!_items.ContainsKey(itemId), "Multiple items with the same ID detected!");
        Debug.Assert(itemId != "", "Item ID may not be blank!");

        _items[itemId] = this;
    }
}