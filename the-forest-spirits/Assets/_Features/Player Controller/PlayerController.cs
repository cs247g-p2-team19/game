using UnityEngine;

/** Template for all PlayerControllers. */
public abstract class PlayerController : AutoMonoBehaviour
{
    /** True if the player shouldn't be able to move around. */
    protected bool _stopped = false;
    
    public abstract Vector2 Velocity { get; protected set; }

    public void StopMovement() {
        _stopped = true;
    }

    public void RestartMovement() {
        _stopped = false;
    }
}