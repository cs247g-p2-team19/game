using System;
using System.Collections;
using System.Collections.Generic;
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
    [TextArea] public string text;
    public UnityEvent onStart;
    public UnityEvent onEnd;
}