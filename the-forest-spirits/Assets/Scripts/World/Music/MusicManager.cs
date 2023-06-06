using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {
        get {
            if (_hasInstance) return _instance;
            var go = new GameObject("_music_manager");
            _hasInstance = true;
            _instance = go.AddComponent<MusicManager>();
            return _instance;
        }
    }

    public float crossfade = 2f;

    public float maxVolume = 0.3f;

    private static bool _hasInstance = false;
    private static MusicManager _instance;

    private static readonly Utility.LerpFn<float> _lerpFn = Mathf.Lerp;


    private AudioSource _sfxSource;
    private List<AudioSource> _audios = new();
    private MusicLayer[] _currentLayers;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        _sfxSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update() {
        var camera = Camera.current;
        if (camera == null) return;
        transform.position = camera.transform.position;
    }

    public Coroutine Play(MusicLayers layers) {
        return StartCoroutine(PlayAsync(layers));
    }

    private IEnumerator PlayAsync(MusicLayers layers) {
        Debug.Log("PLAYING!");
        if (_currentLayers != null) {
            Stop();
        }

        _currentLayers = layers.layers;

        for (int i = _audios.Count; i < _currentLayers.Length; i++) {
            _audios.Add(gameObject.AddComponent<AudioSource>());
        }

        double time = AudioSettings.dspTime;
        var coros = new List<Coroutine>();

        for (int i = 0; i < _currentLayers.Length; i++) {
            _audios[i].clip = _currentLayers[i].clip;
            _audios[i].volume = 0f;
            _audios[i].loop = true;
            if (_currentLayers[i].automaticallyEnabled) {
                _audios[i].PlayScheduled(time);
                int id = i;
                coros.Add(this.AutoLerp(0f, _currentLayers[id].volume, crossfade, _lerpFn,
                    volume => _audios[id].volume = volume));
            }

            if (i != 0) {
                _audios[i].timeSamples = _audios[0].timeSamples;
            }
        }

        foreach (var coro in coros) {
            yield return coro;
        }
    }

    public void PlaySFX(AudioClip sfx) {
        PlaySFX(sfx, 1f);
    }

    public void PlaySFX(AudioClip sfx, float volumeScale) {
        _sfxSource.PlayOneShot(sfx, volumeScale);
    }

    private IEnumerator StopAsync() {
        var audios = _audios;
        _audios = new();
        var coros = new List<Coroutine>();
        foreach (var audio in audios) {
            var coro = this.AutoLerp(audio.volume, 0f, crossfade, _lerpFn, volume => audio.volume = volume);
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
        Debug.Log("STOPPING!");
        return StartCoroutine(StopAsync());
    }

    public void EnableLayer(int id) {
        Debug.Log($"ENABLING LAYER {id} of {_audios.Count}!!");

        var audio = _audios[id];

        audio.volume = 0;
        audio.Play();
        audio.timeSamples = _audios[0].timeSamples;
        this.AutoLerp(0f, _currentLayers[id].volume, crossfade, _lerpFn,
            volume => audio.volume = volume);
    }

    public void DisableLayer(int id) {
        Debug.Log($"DISABLING LAYER {id}!!");

        var coro = this.AutoLerp(_currentLayers[id].volume, 0f, crossfade, _lerpFn,
            volume => _audios[id].volume = volume);
        this.WaitThen(coro, () => { _audios[id].Stop(); });
    }

    private void RecombobulateVolumes() {
        float volSum = 0f;
        for (int i = 0; i < _currentLayers.Length; i++) {
            if (_audios[i].isPlaying) {
                volSum += _currentLayers[i].volume;
            }
        }
        
    }
    
}