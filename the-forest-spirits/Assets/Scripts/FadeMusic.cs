using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum FadeMode
{
    OnStart,
    OnScript
}

[RequireComponent(typeof(AudioSource))]
public class FadeMusic : AutoMonoBehaviour
{
    public FadeMode fadeMode;

    public float fadeTime = 1f;

    [Range(0f, 1f)]
    public float inVolume = 1f;

    public float delayTimeIn = 0f;

    [Range(0f, 1f)]
    public float outVolume = 0f;

    public bool playOnFadeIn = true;

    [AutoDefault, ReadOnly]
    public AudioSource source;

    private Coroutine _coroutine;

    private void Start() {
        if (fadeMode == FadeMode.OnStart) {
            source.volume = outVolume;
            FadeIn();
        }
    }

    public void FadeIn() {
        if (_coroutine != null) {
            StopCoroutine(_coroutine);
        }

        if (playOnFadeIn && !source.isPlaying && source.clip != null) {
            source.Play();
        }

        if (delayTimeIn != 0f) {
            this.WaitThen(delayTimeIn, () => {
                _coroutine = this.AutoLerp(source.volume, inVolume, fadeTime, Mathf.Lerp,
                    value => source.volume = value);
            });
        }
        else {
            _coroutine = this.AutoLerp(source.volume, inVolume, fadeTime, Mathf.Lerp,
                value => source.volume = value);
        }
    }

    public void FadeOut() {
        if (_coroutine != null) {
            StopCoroutine(_coroutine);
        }

        _coroutine = this.AutoLerp(source.volume, outVolume, fadeTime, Mathf.Lerp, value => source.volume = value);
    }
}