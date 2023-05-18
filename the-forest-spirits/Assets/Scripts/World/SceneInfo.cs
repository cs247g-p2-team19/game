using UnityEngine;

public class SceneInfo : AutoMonoBehaviour
{
    public static SceneInfo Instance => FindObjectOfType<SceneInfo>();
    
    public AudioClip backgroundAudio;

    public AudioClip doorOpen;
    public AudioClip doorClosed;

    public AudioClip defaultOnCollectCollectable;
}
