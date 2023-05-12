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
            if (!Lil.Guy.HasItem(spot.spotFor)) continue;

            spot.Activate();
        }
    }

    public void Hide() {
        foreach (var spot in _spots) {
            if (!Lil.Guy.HasItem(spot.spotFor)) continue;
            
            spot.Deactivate();
        }
        display.SetActive(false);
    }
}