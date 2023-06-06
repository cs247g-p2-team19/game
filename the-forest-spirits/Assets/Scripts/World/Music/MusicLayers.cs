using System;
using UnityEngine;

public class MusicLayers : AutoMonoBehaviour
{
    public MusicLayer[] layers;
}

[Serializable]
public class MusicLayer
{
    public AudioClip clip;
    public bool automaticallyEnabled;

    [Range(0f, 1f)]
    public float volume = 1f;
}