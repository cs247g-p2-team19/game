using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
 * Extension to the ForestPlayerController that controls and manages mouse movement.
 */
[RequireComponent(typeof(ForestPlayerController))]
public class MouseManager : AutoMonoBehaviour
{
    private static readonly int MouseDown = Animator.StringToHash("MouseDown");
    private static readonly int IsHovering = Animator.StringToHash("IsHovering");

    #region Unity fields

    [Required]
    public RectTransform cursor;

    [Required]
    public Animator cursorAnim;

    [AutoDefaultMainCamera, ReadOnly]
    public Camera mainCamera;

    [AutoDefault, ReadOnly]
    public ForestPlayerController controller;

    #endregion

    // Since the animation controller can be overridden, this stores the original one
    private RuntimeAnimatorController _originalController;
    private bool _pointerIsDown = false;

    // Stores the elements we're currently hovering over so we can trigger their on-hover-leave listeners
    private readonly HashSet<IMouseEventReceiver> _currentHovers = new();
    private readonly RaycastHit2D[] _resultsBuf = new RaycastHit2D[20];

    #region Unity Events

    private void OnEnable() {
        _originalController = cursorAnim.runtimeAnimatorController;
        Cursor.visible = false;
    }

    private void OnDisable() {
        Cursor.visible = true;
    }

    /** Automatically sets the animator based on the cursor and vice versa */
    protected override void OnValidate() {
        base.OnValidate();
        if (cursorAnim.IsUnityNull() && !cursor.IsUnityNull()) {
            cursorAnim = cursor.GetComponent<Animator>();
        }

        if (cursor.IsUnityNull() && !cursorAnim.IsUnityNull()) {
            cursor = cursorAnim.GetComponent<RectTransform>();
        }
    }

    #endregion


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

    #region Private helper functions

    /**
     * Retrieves all the IMouseEventReceivers that are "valid" (i.e. we're
     * hovering over thema and their IsMouseInteractableAt callbacks return true)
     */
    private List<IMouseEventReceiver> GetValidClickables(Vector2 screenPos) {
        List<IMouseEventReceiver> clickables = new();

        Ray toCast = mainCamera.ScreenPointToRay(screenPos);
        var size = Physics2D.RaycastNonAlloc(toCast.origin, toCast.direction, _resultsBuf, Mathf.Infinity);

        for (int i = 0; i < size; i++) {
            Collider2D collider = _resultsBuf[i].collider;
            foreach (var clickable in collider.GetComponentsInParent<IMouseEventReceiver>()) {
                bool validHover = clickable.IsMouseInteractableAt(screenPos, mainCamera);
                if (!validHover) continue;
                clickables.Add(clickable);
            }
        }

        return clickables;
    }

    /** Triggers OnPointerDown on the very first IMouseEventReceiver that handles it */
    private bool DoMouseDown(List<IMouseEventReceiver> clickables, Vector2 screenPos) {
        foreach (IMouseEventReceiver clickable in clickables) {
            if (clickable.OnPointerDown(screenPos, mainCamera)) {
                return true;
            }
        }

        return false;
    }

    /** Triggers OnPointerUp on the very first IMouseEventReceiver that handles it */
    private bool DoMouseUp(List<IMouseEventReceiver> clickables, Vector2 screenPos) {
        foreach (IMouseEventReceiver clickable in clickables) {
            if (clickable.OnPointerUp(screenPos, mainCamera)) {
                return true;
            }
        }

        return false;
    }

    /** Updates the set of interactables we're hovering over and calls appropriate callbacks */
    private void UpdateHovers(List<IMouseEventReceiver> clickablesOver, Vector2 screenPos) {
        HashSet<IMouseEventReceiver> toRemove = new();
        foreach (IMouseEventReceiver clickable in _currentHovers) {
            if (clickablesOver.Contains(clickable)) continue;
            toRemove.Add(clickable);
            clickable.OnPointerExit(screenPos, mainCamera);
        }

        _currentHovers.RemoveWhere((x) => toRemove.Contains(x));

        foreach (IMouseEventReceiver clickable in clickablesOver) {
            if (!_currentHovers.Add(clickable)) continue;
            clickable.OnPointerEnter(screenPos, mainCamera);
        }
    }

    #endregion
}