using UnityEngine;

public class SceneInfo : MonoBehaviour
{
    public static SceneInfo Instance => FindObjectOfType<SceneInfo>();
    
    public AudioClip backgroundAudio;

    public AudioClip doorOpen;
    public AudioClip doorClosed;

    public AudioClip defaultOnCollectCollectable;
}
