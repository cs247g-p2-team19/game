using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderMenu : AutoMonoBehaviour, IMouseEventReceiver
{
    // Start is called before the first frame update
    
    public bool IsOpen => animator.GetBool(Open);
    
    [AutoDefault, ReadOnly]
    public Animator animator;
    private static readonly int Open = Animator.StringToHash("Open");


    public void OpenFolder() {
        animator.SetBool(Open, true);
    }

    public void CloseFolder() {
        animator.SetBool(Open, false);
    }

    public bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, IMouseAttachable receiver = null) {
        return receiver == null && !IsOpen;
    }

    public bool OnPointerDown(Vector2 screenPos, Camera cam) {
        OpenFolder();
        return true;
    }
}
