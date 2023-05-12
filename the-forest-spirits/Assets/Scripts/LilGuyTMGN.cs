using System;
using UnityEngine;
using UnityEngine.Events;

public class LilGuyTMGN : MonoBehaviour
{
    public Inventory inventory;
    
    public UnityEvent<Collectable> onCollectAny;
    public UnityEvent<InventoryItem> onCollectItem;
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision!");
        var collectable = other.gameObject.GetComponent<Collectable>();
        if (!collectable || !collectable.canBeCollected) return;
        
        collectable.onCollect.Invoke(this);
        onCollectAny.Invoke(collectable);
        
        if (!collectable.IsItem) return;
        var item = other.gameObject.GetComponent<InventoryItem>();
        onCollectItem.Invoke(item);
    }
}