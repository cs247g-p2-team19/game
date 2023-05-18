using UnityEngine;

/**
 * Holds info that's useful for setting up a new scene.
 * There should only ever be one per scene.
 */
public class SceneInfo : AutoMonoBehaviour
{
    public static SceneInfo Instance => FindObjectOfType<SceneInfo>();
    
    public AudioClip backgroundAudio;

    public AudioClip doorOpen;
    public AudioClip doorClosed;

    public AudioClip defaultOnCollectCollectable;
}
