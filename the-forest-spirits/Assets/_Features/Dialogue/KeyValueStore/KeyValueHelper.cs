    public class KeyValueHelper : AutoMonoBehaviour
    {
        public KVStoreKey key;

        public void Set(string value) {
            KeyValueStore.Instance.Set(key, value);
        }
    }