using UnityEngine;

/**
 * Represents a direction that a piece of
 * dialogue can go!
 */
public abstract class Branch : AutoMonoBehaviour
{
    /** Returns the conversation this branch leads to! */
    public abstract Conversation GetConversation();
}