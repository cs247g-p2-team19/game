using System.Collections.Generic;
using UnityEngine;

public class ZStack : AutoMonoBehaviour
{
    public float minLocalZ;
    public float maxLocalZ;

    private List<ZStackItem> _managedStack = new();

    public void MoveToTop(ZStackItem item) {
        var idx = _managedStack.IndexOf(item);
        if (idx != -1) {
            _managedStack.RemoveAt(idx);
        }

        _managedStack.Add(item);
        ApplyZValues();
    }

    public void MoveToBottom(ZStackItem item) {
        var idx = _managedStack.IndexOf(item);
        if (idx != -1) {
            _managedStack.RemoveAt(idx);
        }

        _managedStack.Insert(0, item);
        ApplyZValues();
    }

    private void ApplyZValues() {
        for (var i = 0; i < _managedStack.Count; i++) {
            Vector3 pos = _managedStack[i].transform.localPosition;
            _managedStack[i].transform.localPosition = new Vector3(pos.x, pos.y, ZStop(i));
            _managedStack[i].debugStackState = $"position {i+1} of {_managedStack.Count}";
        }
    }

    private float ZStop(int idx) {
        if (_managedStack.Count == 0) return minLocalZ;
        
        return idx * ((maxLocalZ - minLocalZ) / _managedStack.Count);
    }
}