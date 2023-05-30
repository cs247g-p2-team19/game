/**
 * Represents a direction that a piece of
 * dialogue can go!
 */
public abstract class Branch : AutoMonoBehaviour
{
    /** Returns the conversation this branch leads to! */
    public abstract Conversation GetConversation();
}

public static class BranchExtension
{
    public static Conversation GetNullableConversation(this Branch b) {
        return b == null ? null : b.GetConversation();
    }
}