using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Stencil : InventoryItem, IMouseAttachable
{
    public string targetWord;

    public GameObject cursorAttachment;

    public GameObject thenSpawn;
    public GameObject poof;

    public override bool IsMouseInteractableAt(Vector2 screenPos, Camera cam, IMouseAttachable receiver) {
        return receiver == null;
    }

    public bool OnTryAttach(MouseManager manager) {
        GameObject go = manager.SetCursorAttachment(cursorAttachment);
        go.GetComponentInChildren<TextMeshProUGUI>().text = targetWord;

        return true;
    }

    public bool OnClickWhileAttached(List<IMouseEventReceiver> others, MouseManager manager) {
        if (others.OfType<Word>().FirstOrDefault(w => w.CurrentWord.ToLower() == targetWord) is var word &&
            word != null) {
            var spawned = (GameObject) Instantiate(poof, transform, instantiateInWorldSpace: true);
            spawned.GetComponent<Poof>().objectToCreate = thenSpawn;
            spawned.transform.SetParent(transform.parent, worldPositionStays: true);
            
            manager.RemoveCursorAttachment();
            Destroy(word.gameObject);
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}