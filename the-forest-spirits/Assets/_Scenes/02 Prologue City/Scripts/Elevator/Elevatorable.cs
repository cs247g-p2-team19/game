using System;
using UnityEngine;

public class Elevatorable : AutoMonoBehaviour
{
    [Required]
    public Elevator elevator;

    private void Update() {
        bool contains = elevator.rect.GetWorldBounds().Contains2D(transform.position);
        if (!contains) return;
        
        float direction = elevator.direction == ElevatorDirection.UP ? 1f : -1f;
        transform.position += Vector3.up * (direction * elevator.speed * Time.deltaTime);
    }
}