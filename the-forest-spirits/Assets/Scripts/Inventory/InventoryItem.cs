using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/**
 * Defines an item that can later be picked up.
 * It should be attached to a GameObject in the Inventory, which
 * will be activated when an associated Collectable is picked up
 * (or Unlock is called on this InventoryItem).
 */
[RequireComponent(typeof(Button))]
public class InventoryItem : MonoBehaviour
{
    #region Registry

    // Since we reference InventoryItems by their IDs, we need a registry
    private static Dictionary<string, InventoryItem> _registry = new();

    public static InventoryItem GetItemById(string id) {
        return _registry.TryGetValue(id, out var item) ? item : null;
    }

    #endregion

    #region Unity fields

    [Tooltip("Unique item ID for this item. Can be referenced elsewhere.")]
    public string itemId;

    public string itemName; // Might be unnecessary?

    [Tooltip("Whether or not the user starts with this item. True if they start without it.")]
    public bool startLocked = true;

    [Header("Events")]
    public UnityEvent<InventoryItem> onUnlock;

    [Tooltip("Invoked when this is clicked in the inventory.")]
    public UnityEvent<InventoryItem> onClick;

    #endregion

    #region Button access

    private Button _button {
        get {
            if (_wasCached) return _cachedButton;
            _wasCached = true;
            _cachedButton = GetComponent<Button>();
            return _cachedButton;
        }
    }

    private Button _cachedButton;
    private bool _wasCached;

    #endregion

    private bool _setupDone = false;

    private void OnDestroy() {
        _registry.Remove(itemId);
    }

    public void Setup() {
        if (_setupDone) return;

        _setupDone = true;
        Debug.Assert(!_registry.ContainsKey(itemId), "Multiple items with the same ID detected!");
        Debug.Assert(itemId != "", "Item ID may not be blank!");

        _registry[itemId] = this;

        if (startLocked) {
            Lock();
        }

        _button.onClick.AddListener(() => onClick.Invoke(this));
    }

    private void Awake() {
        Setup();
    }


    public void Unlock() {
        gameObject.SetActive(true);
        onUnlock.Invoke(this);
        Lil.Guy.onUnlockItem.Invoke(this);
    }

    public void Lock() {
        gameObject.SetActive(false);
    }

    // TODO on click
}