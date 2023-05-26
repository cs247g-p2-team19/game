    using System;
    using UnityEngine;

    public class Ground : AutoMonoBehaviour
    {
        private void OnDrawGizmos() {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, Vector3.left * 1000);
            Gizmos.DrawRay(transform.position, Vector3.right * 1000);
        }
    }