using System;
using UnityEngine;

public class KeepPaperAway : AutoMonoBehaviour
{
    [AutoDefaultWithType]
    public Player player;

    [AutoDefaultWithType]
    public Ground ground;

    public float distanceThreshold;

    public float speed = 1f;

    private float _t;

    private float _moveAwayT = 0;

    private void Update() {
        _t += Time.deltaTime;
        if (transform.position.x - player.transform.position.x < distanceThreshold) {
            _moveAwayT = 0.5f;
            Debug.Log("CLOSE!!");
        }

        float isDown = Math.Abs(transform.position.y - ground.transform.position.y) < 0.25 ? 0f : 1f;

        transform.position += (Vector3.right) * (isDown * Mathf.Sin(2f * Mathf.PI / 4f * _t) * Time.deltaTime);
        transform.position += Vector3.down * (isDown * Time.deltaTime * (0.9f + Mathf.Sin(4f * Mathf.PI / 7f * _t)));
        if (_moveAwayT > 0) {
            transform.position += (Vector3.right) * (Time.deltaTime * speed) + (Vector3.up * (Time.deltaTime * speed / 4f));
            _moveAwayT -= Time.deltaTime;
        }
    }
}