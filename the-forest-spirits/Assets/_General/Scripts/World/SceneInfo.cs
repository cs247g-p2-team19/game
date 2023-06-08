using System;
using UnityEngine;

/**
 * Holds info that's useful for setting up a new scene.
 * There should only ever be one per scene.
 */
public class SceneInfo : AutoMonoBehaviour
{
    public static SceneInfo Instance => FindObjectOfType<SceneInfo>();

    public MusicLayers backgroundAudio;
    public MusicLayersMode musicLayerMode;

    public AudioClip doorOpen;
    public AudioClip doorClosed;

    public AudioClip defaultOnCollectCollectable;

    private void Start() {
        if (backgroundAudio == null) return;
        if (musicLayerMode == MusicLayersMode.Replace || !MusicManager.Instance.Playing) {
            this.WaitThen(MusicManager.Instance.Stop(), () => { MusicManager.Instance.Play(backgroundAudio); });
        }
        else {
            for (var i = 0; i < backgroundAudio.layers.Length; i++) {
                var layer = backgroundAudio.layers[i];
                if (layer.automaticallyEnabled) {
                    MusicManager.Instance.EnableLayer(i);
                }
                else {
                    MusicManager.Instance.DisableLayer(i);
                }
            }
        }
    }
}

public enum MusicLayersMode
{
    Replace = 0,
    AdjustLayers = 1,
}