using UnityEngine;

/**
 * Represents a Door that may open or close.
 */
[RequireComponent(typeof(Animator))]
public class Door : AutoMonoBehaviour
{
    public bool IsOpen => animator.GetBool(Open);

    /** The Scene Index to load when TransitionScene is called */
    public int targetSceneId;
    
    [AutoDefault, ReadOnly]
    public Animator animator;
    private static readonly int Open = Animator.StringToHash("Open");
    
    public void DoorOpen() {
        var sfx = SceneInfo.Instance.doorOpen;
        if (sfx != null) {
            Lil.Guy.PlaySFX(sfx);
        }
        animator.SetBool(Open, true);
    }

    public void DoorClose() {
        var sfx = SceneInfo.Instance.doorClosed;
        if (sfx != null) {
            Lil.Guy.PlaySFX(sfx);
        }
        animator.SetBool(Open, false);
    }

    public void TransitionScene() {
        SceneTransitioner.Instance.Load(targetSceneId);
    }
}
