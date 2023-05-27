using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * Manages the items in the inventory.
 */
public class Inventory : AutoMonoBehaviour
{
    public bool IsOpen { get; private set; }

    public List<InventoryItem> Items => FindObjectsByType<InventoryItem>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
    public List<InventoryItem> UnlockedItems => Items.Where(item => !item.isLocked).ToList();

    public GameObject display;
    
    //private List<InventorySpot> Spots => FindObjectsByType<InventorySpot>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
    //Recaculating the Spots each time causes some issues -- for now it's easier to just manual drag all the InventorySpots in I think. Will
    //to find a fix later
    [FormerlySerializedAs("Spots")]
    public List<InventorySpot> spots;
    
    private Coroutine _showHideRoutine;
    private Vector3 _scale;

    #region Unity Events

    private void Awake() {
        _scale = transform.localScale;
        display.SetActive(true);
        
        foreach (var item in FindObjectsByType<InventoryItem>(FindObjectsInactive.Include,FindObjectsSortMode.None)) {
            item.Setup();
            Items.Add(item);
        }

        display.SetActive(false);
        Lil.Inventory.RenderInventory();
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
        RenderInventory();
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

    public void RenderInventory() {
        for (int i = 0; i < UnlockedItems.Count; i++) {
            InventoryItem item = UnlockedItems[i];
            item.transform.position = spots[i].gameObject.transform.position;
        }
    }

    #endregion
}