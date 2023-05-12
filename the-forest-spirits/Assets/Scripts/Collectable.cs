using System;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public bool IsItem => _item != null;

    public bool canBeCollected = true;
    public string itemName;

    [TextArea] public string itemDescription;

    public UnityEvent<LilGuyTMGN> onCollect;

    private InventoryItem _item;

    private void Awake() {
        _item = GetComponent<InventoryItem>();
        onCollect.AddListener(OnCollect);
    }

    private void OnCollect(LilGuyTMGN player) {
        if (IsItem) {
            _item.onCollect.Invoke(player, _item);
        }
    }
}