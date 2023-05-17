using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(ForestPlayerController))]
public class MouseManager : AutoMonoBehaviour
{
    public RectTransform cursor;
    public Image cursorImage;

    public Sprite idleSprite;
    public Sprite defaultHoverSprite;
    public Sprite clickSprite;

    [AutoDefault(MainCamera = true), ReadOnly]
    public Camera mainCamera;

    [AutoDefault, ReadOnly]
    public ForestPlayerController controller;

    private static readonly int MouseDown = Animator.StringToHash("MouseDown");
    private static readonly int IsHovering = Animator.StringToHash("IsHovering");

    private void OnEnable() {
        Cursor.visible = false;
    }

    private void OnDisable() {
        Cursor.visible = true;
    }

    protected override void OnValidate() {
        base.OnValidate();
        if (cursorImage.IsUnityNull() && !cursor.IsUnityNull()) {
            cursorImage = cursor.GetComponent<Image>();
        }

        if (cursor.IsUnityNull() && !cursorImage.IsUnityNull()) {
            cursor = cursorImage.GetComponent<RectTransform>();
        }
    }

    public void UpdateMouse(Vector2 screenPos, bool isDown) {
        cursor.position = screenPos;

        Sprite nextSprite = idleSprite;

        Ray toCast = mainCamera.ScreenPointToRay(screenPos);
        var hit = Physics2D.Raycast(toCast.origin, toCast.direction, Mathf.Infinity, LayerMask.GetMask("Clickables"));
        IClickable clickable = null;

        if (hit.collider != null) {
            clickable = hit.collider.GetComponentInParent<IClickable>();
        }

        if (clickable != null && clickable.IsClickable(screenPos, mainCamera, out _)) {
            nextSprite = isDown ? clickSprite : defaultHoverSprite;
        }

        cursorImage.sprite = nextSprite;
    }
}