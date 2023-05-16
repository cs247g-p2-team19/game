using System;
using UnityEngine;

public enum FadeMode
{
    OnStart,
    OnScript
}

[RequireComponent(typeof(AudioSource))]
public class FadeMusic : MonoBehaviour
{
    public FadeMode fadeMode;

    public float fadeTime = 1f;

    [Range(0f, 1f)]
    public float inVolume = 1f;

    public float delayTimeIn = 0f;

    [Range(0f, 1f)]
    public float outVolume = 0f;

    public bool playOnFadeIn = true;

    private AudioSource _source {
        get {
            if (_wasCached) return _cachedSource;
            _wasCached = true;
            _cachedSource = GetComponent<AudioSource>();
            return _cachedSource;
        }
    }

    private bool _wasCached;
    private AudioSource _cachedSource;

    private Coroutine _coroutine;

    private void Start() {
        if (fadeMode == FadeMode.OnStart) {
            _source.volume = outVolume;
            FadeIn();
        }
    }

    public void FadeIn() {
        if (_coroutine != null) {
            StopCoroutine(_coroutine);
        }

        if (playOnFadeIn && !_source.isPlaying && _source.clip != null) {
            _source.Play();
        }

        if (delayTimeIn != 0f) {
            this.WaitThen(delayTimeIn, () => {
                _coroutine = this.AutoLerp(_source.volume, inVolume, fadeTime, Mathf.Lerp,
                    value => _source.volume = value);
            });
        }
        else {
            _coroutine = this.AutoLerp(_source.volume, inVolume, fadeTime, Mathf.Lerp,
                value => _source.volume = value);
        }
    }

    public void FadeOut() {
        if (_coroutine != null) {
            StopCoroutine(_coroutine);
        }

        _coroutine = this.AutoLerp(_source.volume, outVolume, fadeTime, Mathf.Lerp, value => _source.volume = value);
    }
}