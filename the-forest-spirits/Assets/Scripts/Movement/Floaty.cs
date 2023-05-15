using UnityEngine;

/**
 * Gently floats this game object up and down.
 */
public class Floaty : MonoBehaviour
{
    public float frequency = 4f;
    public float magnitude = 0.1f;

    private float _t = 0f;

    private void Update() {
        _t += Time.deltaTime;

        float delta = Mathf.Sin(2 * Mathf.PI * _t / frequency);
        
        transform.localPosition = Vector3.up * (delta * magnitude);
    }
}