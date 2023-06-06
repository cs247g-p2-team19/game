public class MusicManagerHelper : AutoMonoBehaviour
{
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
}