using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {
        get {
            if (_hasInstance) return _instance;
            var go = new GameObject("_music_manager");
            _instance = go.AddComponent<MusicManager>();
            return _instance;
        }
    }

    public float crossfade = 1f;

    private static bool _hasInstance = false;
    private static MusicManager _instance;


    private AudioSource _sfxSource;
    private List<AudioSource> _audios = new();
    private MusicLayers _currentLayers;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        _sfxSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update() {
        var camera = Camera.current;
        if (camera == null) return;
        transform.position = camera.transform.position;
    }

    public void Play(MusicLayers layers) {
        if (_currentLayers != null) {
            Stop();
        }

        for (int i = _audios.Count; i < layers.layers.Length; i++) {
            _audios.Add(gameObject.AddComponent<AudioSource>());
        }

        double time = AudioSettings.dspTime + crossfade;

        for (int i = 0; i < layers.layers.Length; i++) {
            _audios[i].clip = layers.layers[i].clip;
            _audios[i].volume = layers.layers[i].volume;
            if (layers.layers[i].automaticallyEnabled) {
                _audios[i].PlayScheduled(time);
            }

            if (i != 0) {
                _audios[i].timeSamples = _audios[0].timeSamples;
            }
        }
    }

    public void PlaySFX(AudioClip sfx) {
        PlaySFX(sfx, 1f);
    }

    public void PlaySFX(AudioClip sfx, float volumeScale) {
        _sfxSource.PlayOneShot(sfx, volumeScale);
    }

    public IEnumerator StopAsync() {
        var audios = _audios;
        _audios = new();
        var coros = new List<Coroutine>();
        foreach (var audio in audios) {
            var coro = this.AutoLerp(audio.volume, 0f, crossfade, Utility.EaseInOutF, volume => audio.volume = volume);
            var then = this.WaitThen(coro, () => {
                audio.Stop();
                Destroy(audio);
            });
            
            coros.Add(then);
        }

        foreach (var coro in coros) {
            yield return coro;
        }
    }

    public Coroutine Stop() {
        return StartCoroutine(StopAsync());
    }

    public void EnableLayer(int id) {
        _audios[id].volume = 0;
        _audios[id].Play();
        _audios[id].timeSamples = _audios[0].timeSamples;
        this.AutoLerp(0f, _currentLayers.layers[id].volume, crossfade, Utility.EaseInOutF,
            volume => _audios[id].volume = volume);
    }

    public void DisableLayer(int id) {
        var coro = this.AutoLerp(_currentLayers.layers[id].volume, 0f, crossfade, Utility.EaseInOutF,
            volume => _audios[id].volume = volume);
        this.WaitThen(coro, () => { _audios[id].Stop(); });
    }
}