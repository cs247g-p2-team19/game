using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Jerky : MonoBehaviour
{
    public float frequency = 0.1f;
    public float magnitude = 0.1f;

    private float _t = 0f;
    
    private void Awake() {
        GameObject sub = new GameObject();
        sub.transform.parent = this.transform.parent;
        sub.transform.localPosition = this.transform.localPosition;
        sub.transform.localScale = this.transform.localScale;
        sub.transform.localRotation = this.transform.localRotation;

        transform.parent = sub.transform;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }

    private void Update() {
        _t += Time.deltaTime;
        if (_t < frequency) {
            return;
        }

        _t = 0;

        float scale = magnitude / transform.lossyScale.y;
        
        transform.localPosition = Random.onUnitSphere * scale;
    }
}