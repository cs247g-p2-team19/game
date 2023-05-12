using System;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

public class InventoryItem : MonoBehaviour
{
    public bool isDroppable;
    public bool isConsumable;
    public UnityEvent<InventoryItem> onCollect;
    public UnityEvent<InventoryItem> onUse;
    public UnityEvent<InventoryItem> onDrop;

    private void Awake() {
        onCollect.AddListener(OnCollect);
    }

    private void OnCollect(InventoryItem item) {
        Debug.Assert(item == this);
        
        LilGuyTMGN.PlayerInstance.inventory.AddItem(this);
    }
}