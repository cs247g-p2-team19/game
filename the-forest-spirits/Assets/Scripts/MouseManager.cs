using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ForestPlayerController))]
public class MouseManager : AutoMonoBehaviour
{
    [Required]
    public RectTransform cursor;

    [Required]
    public Animator cursorAnim;

    [AutoDefaultMainCamera, ReadOnly]
    public Camera mainCamera;

    [AutoDefault, ReadOnly]
    public ForestPlayerController controller;

    private static readonly int MouseDown = Animator.StringToHash("MouseDown");
    private static readonly int IsHovering = Animator.StringToHash("IsHovering");

    private RuntimeAnimatorController _originalController;
    private bool _pointerIsDown = false;

    private void OnEnable() {
        _originalController = cursorAnim.runtimeAnimatorController;
        Cursor.visible = false;
    }

    private void OnDisable() {
        Cursor.visible = true;
    }

    protected override void OnValidate() {
        base.OnValidate();
        if (cursorAnim.IsUnityNull() && !cursor.IsUnityNull()) {
            cursorAnim = cursor.GetComponent<Animator>();
        }

        if (cursor.IsUnityNull() && !cursorAnim.IsUnityNull()) {
            cursor = cursorAnim.GetComponent<RectTransform>();
        }
    }

    private readonly HashSet<IClickable> _currentHovers = new();
    private readonly RaycastHit2D[] _resultsBuf = new RaycastHit2D[20];

    private List<IClickable> GetValidClickables(Vector2 screenPos) {
        List<IClickable> clickables = new();

        Ray toCast = mainCamera.ScreenPointToRay(screenPos);
        var size = Physics2D.RaycastNonAlloc(toCast.origin, toCast.direction, _resultsBuf, Mathf.Infinity);

        for (int i = 0; i < size; i++) {
            Collider2D collider = _resultsBuf[i].collider;
            foreach (var clickable in collider.GetComponentsInParent<IClickable>()) {
                bool validHover = clickable.IsMouseInteractableAt(screenPos, mainCamera);
                if (!validHover) continue;
                clickables.Add(clickable);
            }
        }

        return clickables;
    }

    public void UpdateMouse(Vector2 screenPos, bool isDown) {
        cursor.position = screenPos;

        var validClickables = GetValidClickables(screenPos);
        UpdateHovers(validClickables, screenPos);

        var overrideController = validClickables.Count == 0 ? null : validClickables[0].GetCustomAnimation();
        if (isDown && !_pointerIsDown) {
            DoMouseDown(validClickables, screenPos);
        }
        else if (!isDown && _pointerIsDown) {
            DoMouseUp(validClickables, screenPos);
        }

        _pointerIsDown = isDown;

        cursorAnim.runtimeAnimatorController = overrideController == null ? _originalController : overrideController;
        cursorAnim.SetBool(MouseDown, isDown);
        cursorAnim.SetBool(IsHovering, validClickables.Count > 0);
    }

    private bool DoMouseDown(List<IClickable> clickables, Vector2 screenPos) {
        foreach (IClickable clickable in clickables) {
            if (clickable.OnPointerDown(screenPos, mainCamera)) {
                return true;
            }
        }

        return false;
    }

    private bool DoMouseUp(List<IClickable> clickables, Vector2 screenPos) {
        foreach (IClickable clickable in clickables) {
            if (clickable.OnPointerUp(screenPos, mainCamera)) {
                return true;
            }
        }

        return false;
    }

    private void UpdateHovers(List<IClickable> clickablesOver, Vector2 screenPos) {
        HashSet<IClickable> toRemove = new();
        foreach (IClickable clickable in _currentHovers) {
            if (clickablesOver.Contains(clickable)) continue;
            toRemove.Add(clickable);
            clickable.OnPointerExit(screenPos, mainCamera);
        }

        _currentHovers.RemoveWhere((x) => toRemove.Contains(x));

        foreach (IClickable clickable in clickablesOver) {
            if (!_currentHovers.Add(clickable)) continue;
            clickable.OnPointerEnter(screenPos, mainCamera);
        }
    }
}