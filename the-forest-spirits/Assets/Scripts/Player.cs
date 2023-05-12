using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is the player character.
///
/// The player character instance can always be
/// accessed with `Player.PlayerInstance`.
/// </summary>
public class Player : MonoBehaviour
{
    public static Player PlayerInstance { get; private set; }
    
    public Inventory inventory;
    
    public UnityEvent<Collectable> onCollectAny;
    public UnityEvent<InventoryItem> onCollectItem;

    public Player() {
        if (PlayerInstance != null) {
            throw new Exception("There should only ever be one Lil Guy in a scene");
        }
        
        PlayerInstance = this;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision!");
        var collectable = other.gameObject.GetComponent<Collectable>();
        if (!collectable || !collectable.canBeCollected) return;
        
        collectable.onCollect.Invoke();
        onCollectAny.Invoke(collectable);
        
        if (!collectable.IsItem) return;
        var item = other.gameObject.GetComponent<InventoryItem>();
        onCollectItem.Invoke(item);
    }
}