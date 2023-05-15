using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

/// <summary>
/// Defines an item that can be found in the inventory.
/// </summary>

public class InventoryItem : MonoBehaviour
{
    private static Dictionary<string, InventoryItem> _items = new();

    public static InventoryItem GetItemById(string id) {
        return _items.TryGetValue(id, out var item) ? item : null;
    }
    
    public string itemId;
    public string itemName;
    public bool startLocked = true;
    
    public UnityEvent<InventoryItem> onUnlock;
    public UnityEvent<InventoryItem> onClick;

    private void Awake() {
        Debug.Assert(!_items.ContainsKey(itemId), "Multiple items with the same ID detected!");
        Debug.Assert(itemId != "", "Item ID may not be blank!");

        _items[itemId] = this;

        if (startLocked) {
            Lock();
        }
    }
    

    public void Unlock() {
        gameObject.SetActive(true);
        onUnlock.Invoke(this);
        Lil.Guy.onUnlockItem.Invoke(this);
    }

    public void Lock() {
        gameObject.SetActive(false);
    }
}