using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class that manages showing/hiding game objects
/// in the inventory screen.
/// </summary>
public class InventoryDisplay : MonoBehaviour
{
    public bool Open => display.activeInHierarchy;
    public GameObject display;

    private readonly HashSet<InventoryDisplaySpot> _spots = new();
    private readonly Dictionary<InventoryItem, Vector3> _scaleMemory = new();

    private void Awake() {
        display.SetActive(true);
        foreach (var spot in GetComponentsInChildren<InventoryDisplaySpot>()) {
            _spots.Add(spot);
            spot.gameObject.SetActive(false);
        }

        display.SetActive(false);
    }

    public void Toggle() {
        if (Open) Hide();
        else Show();
    }

    public void Show() {
        display.SetActive(true);
        foreach (var spot in _spots) {
            if (LilGuyTMGN.PlayerInstance.inventory.Items.Contains(spot.spotFor)) {
                spot.gameObject.SetActive(true);
                spot.spotFor.transform.parent = spot.transform;
                spot.spotFor.transform.localPosition = Vector3.zero;
                _scaleMemory.Add(spot.spotFor, spot.spotFor.transform.localScale);
                spot.spotFor.transform.localScale = Vector3.one;
                spot.spotFor.gameObject.SetActive(true);
                spot.text.text = spot.spotFor.Collectable.itemName;
            }
        }
    }

    public void Hide() {
        foreach (var item in LilGuyTMGN.PlayerInstance.inventory.Items) {
            item.transform.parent = LilGuyTMGN.PlayerInstance.inventory.transform;
            item.transform.localPosition = Vector3.zero;
            if (_scaleMemory.ContainsKey(item)) {
                item.transform.localScale = _scaleMemory[item];
                _scaleMemory.Remove(item);
            }
            item.gameObject.SetActive(false);
        }

        foreach (var spot in _spots) {
            spot.gameObject.SetActive(false);
        }
        display.SetActive(false);
    }
}