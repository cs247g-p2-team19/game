using UnityEngine.Events;

/**
 * This branch just runs a UnityEvent and passes on to the optional [next]
 */
public class RunCodeBranch : Branch
{
    public UnityEvent onTrigger;
    public Branch next;

    public override Conversation GetConversation() {
        onTrigger.Invoke();

        if (next == null) return null;

        return next.GetConversation();
    }
}