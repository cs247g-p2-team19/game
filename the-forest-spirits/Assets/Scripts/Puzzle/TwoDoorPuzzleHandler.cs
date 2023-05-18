using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class TwoDoorPuzzleHandler : MonoBehaviour
{
    // Start is called before the first frame update
    
    private int _doorsOpened = 0;
    public UnityEvent onPuzzleComplete;
    public Animator animator;

    private static readonly int DoorsOpened = Animator.StringToHash("DoorsOpened");
    public void IncrementDoorsOpened() {
        _doorsOpened += 1;
        animator.SetInteger(DoorsOpened, _doorsOpened);
        if (_doorsOpened == 2) {
            this.WaitThen(1.2f, () => {
                onPuzzleComplete.Invoke();
            });
        }
    }
    
    
}
