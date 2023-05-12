using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Defines a place where a specific item should show up. 
/// </summary>
public class InventoryDisplaySpot : MonoBehaviour
{
    // TODO: What to do for items that aren't in the main scene? May need to be a string or predicate instead.
    public string targetItemId;

    public InventoryItem TargetItem
    {
        get {
            if (_cachedItem != null) return _cachedItem;
            return _cachedItem = InventoryItem.GetItemById(targetItemId);
        }
    }

    public TextMeshProUGUI text;

    public RectTransform target;

    private InventoryItem _cachedItem;
    private Vector3? _previousScale = null;
    private Transform _previousParent = null;
    
    private void Start() {
        Debug.Assert(TargetItem != null, $"Target item could not be found for spot {name}!");
        text.text = TargetItem.Collectable.itemName;
    }

    public void Adopt() {
        _previousParent = TargetItem.transform.parent;
        TargetItem.transform.parent = target.transform;
        TargetItem.transform.localPosition = Vector3.zero;

        _previousScale = TargetItem.transform.localScale;
        TargetItem.transform.localScale = Vector3.one;

        TargetItem.gameObject.SetActive(true);
    }

    public void Deadopt() {
        TargetItem.transform.parent = _previousParent;
        TargetItem.transform.position = Lil.Guy.transform.position + Vector3.right;
        if (_previousScale != null) {
            TargetItem.transform.localScale = _previousScale.Value;
        }
    }

    public void Activate() {
        gameObject.SetActive(true);
    }
    
    public void Deactivate() {
        gameObject.SetActive(false);
    }
}