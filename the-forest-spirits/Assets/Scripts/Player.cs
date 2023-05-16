using System;
using UnityEngine;
using UnityEngine.Events;

/**
 * This is the player character.
 * The player character instance can always be
 * accessed with `Player.PlayerInstance`.
 */
public class Player : MonoBehaviour
{
    /** The global and unique instance of this Player. */
    public static Player Instance {
        get {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<Player>();
            return _instance;
        }
    }

    private static Player _instance;

    public Inventory inventory;

    [Tooltip("Triggered whenever any Collectable is collected")]
    public UnityEvent<Collectable> onCollect;

    [Tooltip("Triggered whenever an item is unlocked")]
    public UnityEvent<InventoryItem> onUnlockItem;

    [Tooltip("Text to fade in/out when the interactable is hovered over.")]
    public FadeText interactPopup;

    public AudioSource audio;

    // Keeps track of what we're overlapping with for when the player later hits the Interact button.
    private Interactable _overlappingInteractable = null;

    #region Unity Events

    private void Start() {
        var info = SceneInfo.Instance;
        if (audio != null) {
            audio.clip = info.backgroundAudio;
            FadeMusic fading = audio.GetComponent<FadeMusic>();
            if (fading != null && fading.playOnFadeIn) {
                fading.FadeIn();
            }
            else {
                audio.Play();
            }
        }
    }

    private void OnDestroy() {
        _instance = null;
    }

    /** Handles collectables and sets up interactions */
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

    /** Unsets interactions when we leave them */
    private void OnTriggerExit2D(Collider2D other) {
        var interactable = other.gameObject.GetComponent<Interactable>();
        if (interactable != null) {
            if (_overlappingInteractable != null) {
                HideInteractablePrompt();
            }

            _overlappingInteractable = null;
        }
    }

    #endregion

    #region Interactables

    private void ShowInteractablePrompt() {
        interactPopup.FadeIn();
    }

    private void HideInteractablePrompt() {
        interactPopup.FadeOut();
    }

    /** Can be called to trigger an interactable */
    public void TriggerInteractions() {
        if (_overlappingInteractable != null) {
            OnInteract(_overlappingInteractable);
        }
    }


    private void OnInteract(Interactable interactable) {
        interactable.Interact();
    }

    #endregion

    #region Audio

    public void PlaySFX(AudioClip clip, float scaleVolume = 1f) {
        audio.PlayOneShot(clip, scaleVolume);
    }

    public void FadeMusicOut() {
        audio.GetComponent<FadeMusic>().FadeOut();
    }

    #endregion
    
}

/** Just some helpers to clean up some code */
public static class PlayerHelpers
{
    public static bool HasItem(this Player p, InventoryItem item) => p.inventory.IsItemUnlocked(item);

    public static bool HasItem(this Player p, string id) => p.inventory.IsItemUnlocked(id);
}