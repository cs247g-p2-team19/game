using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax : MonoBehaviour
{
    public Camera linkedCamera;
    
    [Range(-4f, 4f)]
    public float parallaxFactor = 0f;

    private SpriteRenderer _renderer;
    private Vector3 _lastCamPosition;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        _lastCamPosition = linkedCamera.transform.position;
    }
    

    void Update() {
        Vector3 curCamPosition = linkedCamera.transform.position;
        Vector3 movement = curCamPosition - _lastCamPosition;
        _lastCamPosition = curCamPosition;
        transform.position += movement * parallaxFactor;
    }
}
