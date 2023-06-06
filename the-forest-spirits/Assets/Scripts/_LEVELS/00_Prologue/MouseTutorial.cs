using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseTutorial : AutoMonoBehaviour
{
    public float help1Delay = 10f;

    [AutoDefaultInChildren, Required]
    public CanvasGroup help1Group;

    public float help2Delay = 20f;

    [AutoDefaultInChildren, Required]
    public Animator animator;


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
        this.AutoLerp(0f, 1f, 1f, Utility.EaseInOutF, value => help1Group.alpha = value);

        _coro = this.WaitThen(help2Delay, () => { StartHelp2(); });
    }

    public void StartHelp2() {
        animator.SetTrigger(StartHelp);
    }
}