using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This is the player character.
///
/// The player character instance can always be
/// accessed with `Player.PlayerInstance`.
/// </summary>
public class Player : MonoBehaviour
{
    public static Player PlayerInstance { get; private set; }
    
    public Inventory inventory;
    
    public UnityEvent<Collectable> onCollectAny;
    public UnityEvent<InventoryItem> onCollectItem;
    
    public FadeText interactPopup;

    private Interactable _overlappingInteractable = null;

    public Player() {
        if (PlayerInstance != null) {
            throw new Exception("There should only ever be one Lil Guy in a scene");
        }
        
        PlayerInstance = this;
    }



    private void OnTriggerEnter2D(Collider2D other) {
        var collectable = other.gameObject.GetComponent<Collectable>();
        if (collectable != null && collectable.canBeCollected) {
            collectable.Touch();
            return;
        }

        var interactable = other.gameObject.GetComponent<Interactable>();
        if (interactable != null && interactable.isCurrentlyInteractable) {
            if (_overlappingInteractable == null) {
                ShowInteractablePrompt();
            }

            _overlappingInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        var interactable = other.gameObject.GetComponent<Interactable>();
        if (interactable != null) {
            if (_overlappingInteractable != null) {
                HideInteractablePrompt();
            }
            _overlappingInteractable = null;
        }
    }

    private void ShowInteractablePrompt() {
        interactPopup.FadeIn();
    }

    private void HideInteractablePrompt() {
        interactPopup.FadeOut();
    }
    
    public void TriggerInteractions() {
        if (_overlappingInteractable != null) {
            OnInteract(_overlappingInteractable);
        }
    }

    private void OnInteract(Interactable interactable) {
        interactable.Interact();
    }
}