using UnityEngine;

/**
 * Branch that allows branching text based on how many times
 * this Branch has been taken. It can optionally loop, or
 * fall back to the [atEnd] branch.
 */
public class MultipleConversationsBranch : Branch
{
    public Branch[] branches;
    public bool loop = false;

    [Tooltip("Only if loop is false")]
    public Branch atEnd;

    [SerializeField, ReadOnly]
    public int _numConversations = 0;

    public override Conversation GetConversation() {
        if (_numConversations >= branches.Length) {
            if (atEnd == null) return null;
            return atEnd.GetConversation();
        }

        Conversation next = branches[_numConversations].GetConversation();
        _numConversations++;
        if (loop && _numConversations >= branches.Length) _numConversations = 0;

        return next;
    }
}