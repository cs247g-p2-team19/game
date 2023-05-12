using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public HashSet<InventoryItem> Items { get; } = new HashSet<InventoryItem>();
    
    private void OnEnable() {
        Items.Clear();
        foreach (var item in GetComponentsInChildren<InventoryItem>()) {
            Items.Add(item);
        }
    }

    public void AddItem(InventoryItem item) {
        Items.Add(item);
        if (item.isConsumable) {
            item.onUse.AddListener(DropItem);
        }
        
        item.transform.parent = this.transform;
        var collider = item.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;
        item.transform.localPosition = Vector3.zero + Vector3.forward;

        item.gameObject.SetActive(false);
    }
    
    public void DropItem(LilGuyTMGN player, InventoryItem item) {
        Items.Remove(item);
        item.onDrop.Invoke(player, item);
        
        var collider = item.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = true;
        // TODO: Also move the object away so that the Lil Guy doesn't pick it up again.

        item.gameObject.SetActive(true);
    }
}