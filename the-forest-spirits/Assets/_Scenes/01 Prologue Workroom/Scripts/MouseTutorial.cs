using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseTutorial : AutoMonoBehaviour
{
    public float help1Delay = 10f;

    public Animator help1Animator;
    
    private Coroutine _coro;
    private static readonly int StartHelp = Animator.StringToHash("StartHelp");

    private void Start() {
        _coro = this.WaitThen(help1Delay, () => { StartHelp1(); });
    }

    public void Cancel() {
        help1Animator.enabled = false;
        if (_coro == null) return;
        
        StopCoroutine(_coro);
        _coro = null;
    }

    public void Restart() {
        Start();
    }

    public void StartHelp1() {
        help1Animator.SetTrigger(StartHelp);
    }
}