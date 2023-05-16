using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/** I made this script to put a delay on moving the camera for like bonus cool points or whatever
* I don't know if it's entirely necessary but hehe xd whatever
*
*/
public class CutsceneDelay : MonoBehaviour
{
    public UnityEvent onDelayEnd;
    public void callOnDelayEnd(float timetoWait) {
        this.WaitThen(timetoWait, () => {
            onDelayEnd.Invoke();
        });
    }
}
