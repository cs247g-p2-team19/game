using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleTextEffect : MonoBehaviour
{
    public Material mat;
    public Vector4 DoodleMaxOffset;
    public float DoodleFrameTime;
    public int DoodleFrameCount;
    public Vector4 DoodleNoiseScale;

    public bool DoodleOn;
    /**
    Idk why this exists but I am following the tutorial for it hahahaha!!
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAll();
    }

    public void SetAll() {
        mat.SetVector("_DoodleMaxOffset", DoodleMaxOffset);
        mat.SetFloat("_DoodleFrameTime", DoodleFrameTime);
        mat.SetInt("_DoodleFrameCount", DoodleFrameCount);
        mat.SetVector("_DoodleNoiseScale", DoodleNoiseScale);

        if (DoodleOn) {
            Shader.EnableKeyword("DOODLE_ON");

        }
        else {
            Shader.DisableKeyword("DOODLE_ON");
        }
    }
}
