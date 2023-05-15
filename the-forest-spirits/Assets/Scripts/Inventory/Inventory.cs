using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/**
 * Manages the items in the inventory.
 */
public class Inventory : MonoBehaviour
{
    public bool IsOpen => display.activeInHierarchy;

    public readonly HashSet<InventoryItem> Items = new();
    public readonly HashSet<InventoryItem> UnlockedItems = new();

    public GameObject display;
    
    #region Unity Events

    private void Awake() {
        display.SetActive(true);
        foreach (var spot in GetComponentsInChildren<InventoryItem>()) {
            Items.Add(spot);
        }
        display.SetActive(false);
    }

    #endregion


    #region Item Management

    public void Unlock(InventoryItem item) {
        UnlockedItems.Add(item);
        item.Unlock();
    }

    public bool IsItemUnlocked(InventoryItem item) {
        return UnlockedItems.Contains(item);
    }

    public bool IsItemUnlocked(string id) {
        return UnlockedItems.Contains(InventoryItem.GetItemById(id));
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