using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public bool Open => display.activeInHierarchy;
    public GameObject display;

    private HashSet<InventoryDisplaySpot> _spots = new();

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
                spot.spotFor.gameObject.SetActive(true);
                spot.text.text = spot.spotFor.name;
            }
        }
    }

    public void Hide() {
        foreach (var item in LilGuyTMGN.PlayerInstance.inventory.Items) {
            item.transform.parent = LilGuyTMGN.PlayerInstance.inventory.transform;
            item.transform.localPosition = Vector3.zero;
            item.gameObject.SetActive(false);
        }

        foreach (var spot in _spots) {
            spot.gameObject.SetActive(false);
        }
        display.SetActive(false);
    }
}