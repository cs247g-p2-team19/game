using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Floaty : MonoBehaviour
{
    public float frequency = 4f;
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

        float delta = Mathf.Sin(2 * Mathf.PI * _t / frequency);
        float scale = magnitude / transform.lossyScale.y;
        
        transform.localPosition = Vector3.up * (scale * delta);
    }
}