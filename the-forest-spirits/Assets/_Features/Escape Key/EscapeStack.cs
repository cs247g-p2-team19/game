using System;
using System.Collections.Generic;
using UnityEngine;

public class EscapeStack : AutoMonoBehaviour
{
    public static EscapeStack Instance {
        get {
            if (_hasInstance) return _instance;

            _hasInstance = true;
            var go = new GameObject("_escape_stack");
            _instance = go.AddComponent<EscapeStack>();
            return _instance;
        }
    }

    private static EscapeStack _instance;
    private static bool _hasInstance;


    public delegate void OnEscapeFn();

    public List<OnEscapeFn> OnEscapeFns = new();

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void DoEscape() {
        if (OnEscapeFns.Count == 0) return;
        OnEscapeFn next = OnEscapeFns[^1];
        OnEscapeFns.RemoveAt(OnEscapeFns.Count - 1);
        next();
    }

    public void AddEscape(OnEscapeFn fn) {
        OnEscapeFns.Add(fn);
    }

    public void ClearStack() {
        OnEscapeFns.Clear();
    }

    public void RemoveEscape(OnEscapeFn fn) {
        OnEscapeFns.Remove(fn);
    }
}