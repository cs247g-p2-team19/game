using UnityEngine;

/// <summary>
/// An Interactable object is one that the
/// player (LilGuyTMGN) can trigger by pressing
/// the interact button.
///
/// To make an object interactable, it should implement
/// this interface.
/// </summary>
public interface IInteractable
{
    public void OnInteract() { }
}