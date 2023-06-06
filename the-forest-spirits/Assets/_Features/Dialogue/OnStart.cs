using UnityEngine.Events;

/**
 * Invokes the given callback when this script wakes up.
 */
public class OnStart : AutoMonoBehaviour
{
    public UnityEvent callback;

    void Start() {
        callback.Invoke();
    }
}