public class MusicManagerHelper : AutoMonoBehaviour
{
    public void Stop() {
        MusicManager.Instance.Stop();
    }

    public void Play(MusicLayers layers) {
        MusicManager.Instance.Play(layers);
    }
}