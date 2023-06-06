using UnityEngine;

public class MusicManagerHelper : AutoMonoBehaviour
{
    public float sfxDelay = 0f;
    public float sfxVolume = 1f;

    public void Stop() {
        MusicManager.Instance.Stop();
    }

    public void Play(MusicLayers layers) {
        MusicManager.Instance.Play(layers);
    }

    public void EnableLayer(int id) {
        MusicManager.Instance.EnableLayer(id);
    }

    public void DisableLayer(int id) {
        MusicManager.Instance.DisableLayer(id);
    }

    public void PlaySFX(AudioClip sfx) {
        if (sfxDelay == 0f) {
            MusicManager.Instance.PlaySFX(sfx, sfxVolume);
        }
        else {
            MusicManager.Instance.WaitThen(sfxDelay, () => { MusicManager.Instance.PlaySFX(sfx, sfxVolume); });
        }
    }
}