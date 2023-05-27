using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderMenu : AutoMonoBehaviour
{
    // Start is called before the first frame update
    
    public bool IsOpen => animator.GetBool(Open);
    
    [AutoDefault, ReadOnly]
    public Animator animator;
    private static readonly int Open = Animator.StringToHash("Open");


    public void FolderInteract() {
        animator.SetBool(Open, !IsOpen);
    }
    
}
