using UnityEngine;

[RequireComponent(typeof(RectTransform)), RequireComponent(typeof(BoxCollider2D))]
[ExecuteAlways]
public class MatchColliderToRectTransform : AutoMonoBehaviour
{
    [AutoDefault, ReadOnly]
    public RectTransform rect;

    [AutoDefault, ReadOnly]
    public new BoxCollider2D collider;

    private void Update() {
        collider.size = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
    }
}