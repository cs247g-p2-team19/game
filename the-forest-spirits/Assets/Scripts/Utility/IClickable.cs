using UnityEngine;

public interface IClickable
{
    public bool OnClick(Vector2 screenPos, Camera cam);
}