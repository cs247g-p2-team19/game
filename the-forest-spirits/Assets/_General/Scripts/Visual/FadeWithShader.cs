using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWithShader : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToFade;
    private Material _mat;
    private static readonly int Alpha = Shader.PropertyToID("_Alpha");
    private Coroutine _currentAlpha;

    void OnEnable() {
        _mat = this.gameObject.GetComponent<Renderer>().material;
        _currentAlpha = this.AutoLerp(0, 1, timeToFade,
            Utility.EaseInOut<float>(Mathf.Lerp),
            SetAlpha); 
    }

    private void SetAlpha(float val) {
        _mat.SetFloat(Alpha, val);
        Debug.Log(val);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
