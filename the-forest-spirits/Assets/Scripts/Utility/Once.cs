using UnityEngine;
using UnityEngine.Events;

public class Once : AutoMonoBehaviour
{
    public UnityEvent onTrigger;

    [Header("Debug")]
    [SerializeField, ReadOnly]
    private bool _didTrigger;

    public void Trigger() {
        if (_didTrigger) return;

        _didTrigger = true;
        onTrigger.Invoke();
    }
}