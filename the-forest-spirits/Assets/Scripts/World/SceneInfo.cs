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

    public AudioClip doorOpen;
    public AudioClip doorClosed;

    public AudioClip defaultOnCollectCollectable;

    private void Start() {
        if (backgroundAudio == null) return;
        this.WaitThen(MusicManager.Instance.Stop(), () => { MusicManager.Instance.Play(backgroundAudio); });
    }
}