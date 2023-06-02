using System;
using UnityEngine;
using UnityEngine.Events;

/**
 * This is the player character.
 * The player character instance can always be
 * accessed with `Player.PlayerInstance`.
 */
public class Player : AutoMonoBehaviour
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

    [AutoDefaultInChildren, Required]
    public Inventory inventory;

    [Tooltip("Triggered whenever any Collectable is collected")]
    public UnityEvent<Collectable> onCollect;

    [Tooltip("Triggered whenever an item is unlocked")]
    public UnityEvent<InventoryItem> onUnlockItem;

    [Tooltip("Text to fade in/out when the interactable is hovered over.")]
    [AutoDefaultInChildren, Required]
    public FadeText interactPopup;

    [AutoDefaultInChildren, Required]
    public new AudioSource audio;
    
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
    }
    
    #endregion

    #region Interactables

    private void ShowInteractablePrompt() {
        interactPopup.FadeIn();
    }

    private void HideInteractablePrompt() {
        interactPopup.FadeOut();
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