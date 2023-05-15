using UnityEngine;

/** The expected state of the door for this Branch (open or closed) */
public enum DoorState
{
    Open,
    Closed
}

/** Represents a branch based on whether a door is open or closed */
public class DoorBranch : Branch
{
    [Header("progress to")]
    public Branch next;
    
    [Header("if")]
    public Door door;
    public DoorState was;
    
    [Header("otherwise")]
    public Branch fallbackTo;


    public override Conversation GetConversation() {
        bool shouldBeOpen = was == DoorState.Open;
        
        if (door.IsOpen == shouldBeOpen) {
            return next.GetConversation();
        }

        return fallbackTo.GetConversation();
    }
}