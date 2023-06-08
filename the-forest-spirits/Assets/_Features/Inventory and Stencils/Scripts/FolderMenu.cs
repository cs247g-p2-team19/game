using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderMenu : AutoMonoBehaviour, IMouseEventReceiver
{
    // Start is called before the first frame update

    public bool IsOpen => animator.GetBool(Open);

    public AudioClip onOpenClip;
    public AudioClip onCloseClip;

    [AutoDefault, ReadOnly]
    public Animator animator;

    private static readonly int Open = Animator.StringToHash("Open");


    public void OpenFolder() {
        KeyValueStore.Instance.Set(KVStoreKey.HasOpenedCaseFile, "true");
        KeyValueStore.Instance.Set(KVStoreKey.FolderOpen, "true");

        animator.SetBool(Open, true);
        if (onOpenClip != null) {
            Lil.Music.PlaySFX(onOpenClip, 0.2f);
        }
    }

    public void CloseFolder() {
        KeyValueStore.Instance.Delete(KVStoreKey.FolderOpen);
        animator.SetBool(Open, false);
        if (onCloseClip != null) {
            Lil.Music.PlaySFX(onCloseClip, 0.2f);
        }
    }

    public bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, IMouseAttachable receiver = null) {
        return receiver == null && !IsOpen;
    }

    public bool OnPointerDown(Vector2 screenPos, Camera cam) {
        OpenFolder();
        return true;
    }

    public float GetScreenOrdering() {
        return transform.position.z;
    }
}