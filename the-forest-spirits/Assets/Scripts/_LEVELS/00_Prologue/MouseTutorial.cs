using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseTutorial : AutoMonoBehaviour
{
    public float help1Delay = 10f;

    public Animator help1Animator;

    public float help2Delay = 20f;

    public Animator help2Animator;


    private Coroutine _coro;
    private static readonly int StartHelp = Animator.StringToHash("StartHelp");

    private void Start() {
        _coro = this.WaitThen(help1Delay, () => { StartHelp1(); });
    }

    public void Cancel() { }

    public void Restart() {
        Start();
    }

    public void StartHelp1() {
        help1Animator.SetTrigger(StartHelp);
        _coro = this.WaitThen(help2Delay, () => { StartHelp2(); });
    }

    public void StartHelp2() {
        help2Animator.SetTrigger(StartHelp);
    }
}