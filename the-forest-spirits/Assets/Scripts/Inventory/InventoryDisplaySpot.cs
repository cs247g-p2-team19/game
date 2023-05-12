using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Defines a place where a specific item should show up. 
/// </summary>
public class InventoryDisplaySpot : MonoBehaviour
{
    // TODO: What to do for items that aren't in the main scene? May need to be a string or predicate instead.
    public InventoryItem spotFor;
    
    public TextMeshProUGUI text;

    public RectTransform target;

    private Vector3? _previousScale = null;
    public void Activate() {
        gameObject.SetActive(true);
        text.text = spotFor.Collectable.itemName;

        spotFor.transform.parent = target.transform;
        spotFor.transform.localPosition = Vector3.zero;

        _previousScale = spotFor.transform.localScale;
        spotFor.transform.localScale = Vector3.one;
            
        spotFor.gameObject.SetActive(true);
    }

    public void Deactivate() {
        Lil.Guy.Adopt(spotFor.gameObject);
        spotFor.transform.localPosition = Vector3.zero;
        if (_previousScale != null) {
            spotFor.transform.localScale = _previousScale.Value;
        }
        spotFor.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
