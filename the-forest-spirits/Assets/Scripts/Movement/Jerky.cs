using UnityEngine;
using Random = UnityEngine.Random;

/** Alternative to Floaty: Jerky! Shakes randomly every _t seconds */
public class Jerky : MonoBehaviour
{
    public float frequency = 0.1f;
    public float magnitude = 0.1f;

    private float _t;

    private void Update() {
        _t += Time.deltaTime;
        if (_t < frequency) {
            return;
        }

        _t = 0;

        transform.localPosition = Random.onUnitSphere * magnitude;
    }
}