using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Stencil : InventoryItem, IMouseAttachable
{
    public string targetWord;

    public GameObject cursorAttachment;

    public GameObject thenSpawn;
    public GameObject poof;

    //[Tooltip("Check for whether or not the associated word will be in menu or out of menu")]
    //public bool useInMenu;
    
    //this is literally just some random gameobject not in the menu, prob will fix later
    public GameObject outside;
    

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
            
            //if (!useInMenu) {
                var spawned = (GameObject)Instantiate(poof, outside.transform, instantiateInWorldSpace: true);
                spawned.GetComponent<Poof>().objectToCreate = thenSpawn;
                spawned.transform.SetParent(outside.transform.parent, worldPositionStays: true);

                manager.RemoveCursorAttachment();
                Destroy(word.gameObject);
                Destroy(gameObject);
                return true;
            //}
        }

        return false;
    }
}