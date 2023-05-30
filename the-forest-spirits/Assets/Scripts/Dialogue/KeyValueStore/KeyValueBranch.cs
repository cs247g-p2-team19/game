using System;
using UnityEngine;

public enum KVCompareType
{
    IsValue,
    ContainsValue,
    HasAnyValue,
    HasNoValue
}

public class KeyValueBranch : Branch
{
    [Header("If")]
    public KVStoreKey key;

    public KVCompareType verb;

    public string value;

    public Branch thenGoTo;
    public Branch otherwiseGoTo;

    private string StoredValue => KeyValueStore.Instance.Get(key);

    public override Conversation GetConversation() {
        return verb switch {
            KVCompareType.IsValue => (StoredValue == value ? thenGoTo : otherwiseGoTo).GetNullableConversation(),
            KVCompareType.ContainsValue => (StoredValue.Contains(value) ? thenGoTo : otherwiseGoTo)
                .GetNullableConversation(),
            KVCompareType.HasAnyValue => (StoredValue != "" ? thenGoTo : otherwiseGoTo).GetNullableConversation(),
            KVCompareType.HasNoValue => (StoredValue == "" ? thenGoTo : otherwiseGoTo).GetNullableConversation(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}