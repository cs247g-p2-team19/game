using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Stencil : InventoryItem, IMouseAttachable
{
    public string targetWord;

    public GameObject cursorAttachment;

    public GameObject thenSpawn;
    public GameObject poof;

    public UnityEvent onStencilUse;

    //[Tooltip("Check for whether or not the associated word will be in menu or out of menu")]
    //public bool useInMenu;
    
    //this is literally just some random gameobject not in the menu, prob will fix later
    [FormerlySerializedAs("outside")]
    public GameObject spawnPoint;
    

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
                var spawned = (GameObject)Instantiate(poof, spawnPoint.transform, instantiateInWorldSpace: true);
                //this way you can set spawn points without needing to do extra stuff i think this is slightly better
                spawned.transform.position = spawnPoint.transform.position;
                spawned.GetComponent<Poof>().objectToCreate = thenSpawn;
                spawned.transform.SetParent(spawnPoint.transform.parent, worldPositionStays: true);

                manager.RemoveCursorAttachment();
                onStencilUse.Invoke();
                Destroy(word.gameObject);
                Destroy(gameObject);
                return true;
            //}
        }

        return false;
    }
}