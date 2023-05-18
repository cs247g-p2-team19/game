using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/**
 * Represents and stores actual dialogue with a Talker!
 */
public class Conversation : Branch
{
    public ConversationPart[] dialogue;

    [Tooltip("Where to go after this Conversation is done")]
    public Branch andThen;

    public UnityEvent onStart;
    public UnityEvent onEnd;

    public override Conversation GetConversation() {
        return this;
    }
}

/**
 * Represents one line of dialogue
 */
[Serializable]
public class ConversationPart
{
    [TextArea]
    public string text;

    [Tooltip("Makes it so that interactions DON'T move to the next line until forced by a script or link")]
    public bool stopUntilForced = false;

    public UnityEvent onStart;
    public LinkResponder[] linkResponders;
    public UnityEvent onEnd;
    public float waitTime = 0f;

    /** Triggers the link with a given ID */
    public void TriggerLink(string id) {
        var evt = linkResponders.First(resp => resp.linkId == id).onLink;
        evt.Invoke(id);
    }
}

/**
 * Represents a response to a link in the text with a given ID
 */
[Serializable]
public class LinkResponder
{
    public string linkId;

    [Tooltip("If true, clicking this link will (force) the next dialogue")]
    public bool thenContinue = true;

    public UnityEvent<string> onLink;
}