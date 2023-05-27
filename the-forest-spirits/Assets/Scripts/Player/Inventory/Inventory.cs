using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Manages the items in the inventory.
 */
public class Inventory : AutoMonoBehaviour
{
    public bool IsOpen { get; private set; }

    public List<InventoryItem> Items => FindObjectsOfType<InventoryItem>().ToList();
    public List<InventoryItem> UnlockedItems => Items.Where(item => !item.isLocked).ToList();

    public GameObject display;
    
    [AutoDefaultInChildren]
    public InventorySpot[] spots;

    private Coroutine _showHideRoutine;
    private Vector3 _scale;

    #region Unity Events

    private void Awake() {
        _scale = transform.localScale;
        display.SetActive(true);
        
        foreach (var item in GetComponentsInChildren<InventoryItem>()) {
            item.Setup();
            Items.Add(item);
        }

        display.SetActive(false);
    }

    #endregion


    #region Item Management
    public bool IsItemUnlocked(InventoryItem item) {
        return UnlockedItems.Contains(item);
    }

    public bool IsItemUnlocked(string id) {
        return UnlockedItems.Contains(InventoryItem.GetItemById(id));
    }

    #endregion


    #region Display Management

    public void Toggle() {
        if (IsOpen) Hide();
        else Show();
    }


    public void Show() {
        IsOpen = true;
        if (_showHideRoutine != null) {
            StopCoroutine(_showHideRoutine);
        }

        transform.localScale = Vector3.zero;
        display.SetActive(true);
        var lerpCoro = this.AutoLerp(Vector3.zero, _scale, 0.5f,
            Utility.EaseOut(Utility.EaseInOut<Vector3>(Vector3.Lerp)), sc => transform.localScale = sc);
        _showHideRoutine = this.WaitThen(lerpCoro, () => { _showHideRoutine = null; });
    }

    public void Hide() {
        IsOpen = false;
        if (_showHideRoutine != null) {
            StopCoroutine(_showHideRoutine);
        }

        transform.localScale = _scale;
        display.SetActive(true);
        var lerpCoro = this.AutoLerp(_scale, Vector3.zero, 0.5f,
            Utility.EaseOut(Utility.EaseInOut<Vector3>(Vector3.Lerp)), sc => transform.localScale = sc);
        _showHideRoutine = this.WaitThen(lerpCoro, () => {
            _showHideRoutine = null;
            display.SetActive(false);
        });
    }

    #endregion
}