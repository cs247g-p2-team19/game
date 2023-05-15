using UnityEngine;

/** Represents a branch based on the player's inventory */
public class ItemBranch : Branch
{
    [Header("progress to")]
    public Branch next;
    
    [Header("if player")]
    public bool doesnt;
    
    [Header("have item with id")]
    public string itemId;
    
    [Header("otherwise")]
    public Branch fallbackTo;
    
    
    public override Conversation GetConversation() {
        if (Lil.Guy.HasItem(itemId) != doesnt) {
            return next.GetConversation();
        }

        return fallbackTo.GetConversation();
    }
}