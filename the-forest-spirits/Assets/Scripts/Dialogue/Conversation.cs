using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Conversation : Branch
{
    public ConversationPart[] dialogue;
    public Branch andThen;
    public UnityEvent onStart;
    public UnityEvent onEnd;

    public override Conversation GetConversation() {
        return this;
    }
}

[Serializable]
public class ConversationPart
{
    [TextArea]
    public string text;

    public bool stopUntilForced = false;
    
    public UnityEvent onStart;
    public UnityEvent onEnd;
    public LinkResponder[] linkResponders;
    public float waitTime = 0f;

    public void TriggerLink(string id) {
        var evt = linkResponders.First(resp => resp.linkId == id).onLink;
        evt.Invoke(id);
    }
}

[Serializable]
public class LinkResponder
{
    public string linkId;
    public UnityEvent<string> onLink;
}