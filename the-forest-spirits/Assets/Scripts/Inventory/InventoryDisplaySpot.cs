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
}
