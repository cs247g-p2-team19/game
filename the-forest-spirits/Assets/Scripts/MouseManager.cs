using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ForestPlayerController))]
public class MouseManager : AutoMonoBehaviour
{
    public RectTransform cursor;
    public Animator cursorAnim;

    [AutoDefaultMainCamera, ReadOnly]
    public Camera mainCamera;

    [AutoDefault, ReadOnly]
    public ForestPlayerController controller;

    private static readonly int MouseDown = Animator.StringToHash("MouseDown");
    private static readonly int IsHovering = Animator.StringToHash("IsHovering");

    private RuntimeAnimatorController _originalController;

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

    public void UpdateMouse(Vector2 screenPos, bool isDown) {
        cursor.position = screenPos;

        Ray toCast = mainCamera.ScreenPointToRay(screenPos);
        var hit = Physics2D.Raycast(toCast.origin, toCast.direction, Mathf.Infinity, LayerMask.GetMask("Clickables"));
        IClickable clickable = null;

        if (hit.collider != null) {
            clickable = hit.collider.GetComponentInParent<IClickable>();
        }

        AnimatorOverrideController overrideController = null;
        bool isHovering = clickable != null && clickable.IsClickable(screenPos, mainCamera, out overrideController);

        if (isHovering && overrideController != null) {
            cursorAnim.runtimeAnimatorController = overrideController;
        }
        else {
            cursorAnim.runtimeAnimatorController = _originalController;
        }

        cursorAnim.SetBool(MouseDown, isDown);
        cursorAnim.SetBool(IsHovering, isHovering);
    }
}