using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected bool _stopped = false;

    public void StopMovement() {
        _stopped = true;
    }

    public void RestartMovement() {
        _stopped = false;
    }
}