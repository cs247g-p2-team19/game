using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the items in the inventory.
///
/// It should be attached to the player object; GameObjects
/// will be moved and stored here until they are ready to
/// be used or alternatively displayed in the inventory screen.
/// </summary>
public class Inventory : MonoBehaviour
{
    public bool IsOpen => display.activeInHierarchy;

    public HashSet<InventoryItem> Items { get; } = new();

    public GameObject display;

    private readonly Dictionary<InventoryItem, InventoryDisplaySpot> _spots = new();

    #region Unity Events

    private void Start() {
        display.SetActive(true);
        foreach (var spot in GetComponentsInChildren<InventoryDisplaySpot>()) {
            _spots.Add(spot.TargetItem, spot);
            spot.Deactivate();
        }

        display.SetActive(false);

        foreach (var item in GetComponentsInChildren<InventoryItem>()) {
            AddItem(item);
        }
    }

    #endregion


    #region Item Management

    public void AddItem(InventoryItem item) {
        Items.Add(item);
        if (item.isConsumable) {
            item.onUse.AddListener(DropItem);
        }

        item.onCollect.Invoke(item);
        
        var collider = item.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        if (_spots.TryGetValue(item, out var spot)) {
            spot.Adopt();
            spot.Activate();
        }
        
        else {
            item.transform.parent = this.transform;
            item.gameObject.SetActive(false);
        }

    }

    public void DropItem(InventoryItem item) {
        Items.Remove(item);
        item.onDrop.Invoke(item);

        var collider = item.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = true;
        
        if (_spots.TryGetValue(item, out var spot)) {
            spot.Deadopt();
            spot.Deactivate();
        }
        
        item.gameObject.SetActive(true);
    }

    public bool Contains(InventoryItem item) {
        return Items.Contains(item);
    }

    public bool Contains(string id) {
        return Items.Contains(InventoryItem.GetItemById(id));
    }

    #endregion


    #region Display Management

    public void Toggle() {
        if (IsOpen) Hide();
        else Show();
    }

    public void Show() {
        display.SetActive(true);
    }

    public void Hide() {
        display.SetActive(false);
    }

    #endregion
}