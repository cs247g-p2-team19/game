using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointFollow : MonoBehaviour
{
    public Transform target; // The object to follow

    private Vector3 initialOffset; // The initial offset between the objects

    private void Start()
    {
        if (target != null)
        {
            // Calculate the initial offset between the objects
            initialOffset = transform.position - target.position;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // Calculate the desired position based on the target's position and initial offset
            Vector3 desiredPosition = target.position + initialOffset;

            // Move towards the desired position
            transform.position = desiredPosition;
        }
    }
}
