using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Defines an object that can be collected.
///
/// It should be given a name in the Unity editor.
/// </summary>
public class Collectable : MonoBehaviour
{
    public bool IsItem => _item != null;

    public bool canBeCollected = true;
    public string itemName;

    [TextArea] public string itemDescription;

    public UnityEvent onCollect;

    private InventoryItem _item;

    private void Awake() {
        _item = GetComponent<InventoryItem>();
        onCollect.AddListener(OnCollect);
    }

    private void OnCollect() {
        if (IsItem) {
            _item.onCollect.Invoke(_item);
        }
    }
}