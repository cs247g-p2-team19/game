using UnityEngine;

public enum DoorState
{
    Open,
    Closed
}

public class DoorBranch : Branch
{
    [Header("progress to")] public Branch next;
    [Header("if")]
    public Door door;
    public DoorState was;
    [Header("otherwise")] public Branch fallbackTo;
    
    
    public override Conversation GetConversation() {
        if (door.IsOpen == (was == DoorState.Open)) {
            return next.GetConversation();
        }

        return fallbackTo.GetConversation();
    }
}