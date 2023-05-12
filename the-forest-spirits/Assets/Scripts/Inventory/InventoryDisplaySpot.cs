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
    [FormerlySerializedAs("spotFor")] public InventoryItem targetItem;
    
    public TextMeshProUGUI text;

    public RectTransform target;

    private Vector3? _previousScale = null;
    public void Activate() {
        gameObject.SetActive(true);
        text.text = targetItem.Collectable.itemName;

        targetItem.transform.parent = target.transform;
        targetItem.transform.localPosition = Vector3.zero;

        _previousScale = targetItem.transform.localScale;
        targetItem.transform.localScale = Vector3.one;
            
        targetItem.gameObject.SetActive(true);
    }

    public void Deactivate() {
        Lil.Guy.Adopt(targetItem.gameObject);
        targetItem.transform.localPosition = Vector3.zero;
        if (_previousScale != null) {
            targetItem.transform.localScale = _previousScale.Value;
        }
        targetItem.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
