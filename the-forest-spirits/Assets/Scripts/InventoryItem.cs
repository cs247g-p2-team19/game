using System;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

public class InventoryItem : MonoBehaviour
{
    public bool isDroppable;
    public bool isConsumable;
    public UnityEvent<LilGuyTMGN, InventoryItem> onCollect;
    public UnityEvent<LilGuyTMGN, InventoryItem> onUse;
    public UnityEvent<LilGuyTMGN, InventoryItem> onDrop;

    private void Awake() {
        onCollect.AddListener(OnCollect);
    }

    private void OnCollect(LilGuyTMGN player, InventoryItem item) {
        Debug.Assert(item == this);
        
        player.inventory.AddItem(this);
    }
}