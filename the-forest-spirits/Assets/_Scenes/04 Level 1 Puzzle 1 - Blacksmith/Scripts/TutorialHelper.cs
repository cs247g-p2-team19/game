using System;
using UnityEngine;

public class TutorialHelper : MonoBehaviour
{
    public GameObject spot1folder;
    public GameObject spot2stencil;
    public GameObject spot3note;


    private void Update() {
        KeyValueStore kv = KeyValueStore.Instance;

        if (kv.Get(KVStoreKey.L4HasNote).Length != 0 && kv.Get(KVStoreKey.L4HasStencil).Length != 0 &&
            kv.Get(KVStoreKey.L4DidManifest).Length == 0) {
            if (kv.Get(KVStoreKey.FolderOpen).Length == 0) {
                spot1folder.SetActive(true);
                spot2stencil.SetActive(false);
                spot3note.SetActive(false);
            }
            else if (kv.Get(KVStoreKey.StencilAttached).Length == 0) {
                spot1folder.SetActive(false);
                spot2stencil.SetActive(true);
                spot3note.SetActive(false);
            }
            else {
                spot1folder.SetActive(false);
                spot2stencil.SetActive(false);
                spot3note.SetActive(true);
            }
        }
        else {
            spot1folder.SetActive(false);
            spot2stencil.SetActive(false);
            spot3note.SetActive(false);
        }
    }
}