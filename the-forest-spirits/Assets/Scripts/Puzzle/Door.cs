using UnityEngine;
using UnityEngine.Events;

/**
 * Represents a Door that may open or close.
 */
[RequireComponent(typeof(Animator))]
public class Door : AutoMonoBehaviour
{
    public bool IsOpen => animator.GetBool(Open);

    /** The Scene Index to load when TransitionScene is called */
    public int targetSceneId;
    
    /** The Scene Path to load when TransitionScene is called, for ease of testing */
    public string targetScenePath;
    
    [AutoDefault, ReadOnly]
    public Animator animator;
    private static readonly int Open = Animator.StringToHash("Open");

    /** Event for puzzles that may correspond to only when a door opens w/ no dialogue */
    /** There honestly might be a much better way to do this but idk */
    public UnityEvent onOpen;
    
    public void DoorOpen() {
        var sfx = SceneInfo.Instance.doorOpen;
        if (sfx != null) {
            Lil.Guy.PlaySFX(sfx);
        }
        onOpen.Invoke();
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

    public void TransitionSceneWithPath() {
        SceneTransitioner.Instance.LoadFromPath(targetScenePath);
    }

    /**
     * Function to check if door is closed -> then open; if door is already open, then transition scene
     */
    public void NoDialogueDoorInteract() {
        if (this.IsOpen) {
            TransitionScene();
        }
        else {
            DoorOpen();
        }
    }
    public void NoDialogueDoorInteractWithPath() {
        if (this.IsOpen) {
            TransitionSceneWithPath();
        }
        else {
            DoorOpen();
        }
    }
    
}
