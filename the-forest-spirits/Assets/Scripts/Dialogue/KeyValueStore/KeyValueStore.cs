using System.Collections.Generic;
using UnityEngine;

public class KeyValueStore : AutoMonoBehaviour
{
    public static KeyValueStore Instance {
        get {
            if (_hasInstance) return _instance;

            _hasInstance = true;
            var go = new GameObject("_key_value_store");
            _instance = go.AddComponent<KeyValueStore>();
            return _instance;
        }
    }

    private static KeyValueStore _instance;
    private static bool _hasInstance;

    private Dictionary<KVStoreKey, string> _values = new();

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void Set(KVStoreKey key, string value) {
        _values[key] = value;
    }

    public void Delete(KVStoreKey key) {
        _values.Remove(key);
    }

    public string Get(KVStoreKey key) {
        return _values.TryGetValue(key, out var value) ? value : "";
    }
}