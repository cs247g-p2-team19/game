using System;
using UnityEngine;

public enum ElevatorDirection
{
    UP,
    DOWN
}

[RequireComponent(typeof(RectTransform))]
public class Elevator : AutoMonoBehaviour
{
    [AutoDefault]
    public RectTransform rect;
    
    public ElevatorDirection direction = ElevatorDirection.UP;
    public float speed;
}