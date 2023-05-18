using UnityEngine;
using Random = UnityEngine.Random;

/** Alternative to Floaty: Jerky! Shakes randomly every _t seconds */
public class Jerky : AutoMonoBehaviour
{
    public float frequency = 0.1f;
    public float magnitude = 0.1f;
    public bool lockZ = true;

    private float _t;

    private void Update() {
        _t += Time.deltaTime;
        if (_t < frequency) {
            return;
        }

        _t = 0;

        Vector3 jerkPosition = Random.onUnitSphere * magnitude;
        if (lockZ) {
            jerkPosition.z = 0;
        }
        
        transform.localPosition = jerkPosition;
    }
}