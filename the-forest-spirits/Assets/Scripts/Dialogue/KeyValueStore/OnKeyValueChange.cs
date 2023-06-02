using System;
using UnityEngine;
using UnityEngine.Events;

public enum OnChangePredicate
{
    BecomesEqual = 0,
    BecomesNonEmpty = 1,
    BecomesEmpty = 2
}

public class OnKeyValueChange : MonoBehaviour
{
    public KVStoreKey key;
    public OnChangePredicate when = OnChangePredicate.BecomesEqual;
    public string to;
    public UnityEvent then;

    private void Awake() {
        KeyValueStore.Instance.onChange.AddListener(OnChange);
    }

    private void OnChange(KVStoreKey changedKey, string oldValue, string newValue) {
        if (changedKey != key) return;
        if (oldValue == newValue) return;
        
        switch (when) {
            case OnChangePredicate.BecomesEqual when newValue == to:
                then.Invoke();
                break;
            case OnChangePredicate.BecomesNonEmpty when newValue.Length != 0:
                then.Invoke();
                break;
            case OnChangePredicate.BecomesEmpty when newValue.Length == 0:
                then.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
