using UnityEngine;

public interface IClickable
{
    public bool OnClick(Vector2 screenPos, Camera cam);

    public bool IsClickable(Vector2 screenPos, Camera cam, out Sprite customSprite);

}