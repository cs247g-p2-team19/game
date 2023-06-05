using UnityEngine;
using UnityEngine.SceneManagement;

/** Orchestrates leaving a scene and going to another scene. */
public class SceneTransitioner : AutoMonoBehaviour
{
    public static SceneTransitioner Instance => FindObjectOfType<SceneTransitioner>();

    [AutoDefaultInChildren, Required]
    public FadeImage imageFader;

    public float pauseTime = 1f;

    private bool _isLoading = false;

    public void Load(int idx) {
        if (_isLoading) return;

        _isLoading = true;

        if (pauseTime > 0f) {
            imageFader.FadeIn();
            Lil.Guy.FadeMusicOut();
            var ready = SceneManager.LoadSceneAsync(idx, LoadSceneMode.Single);
            ready.allowSceneActivation = false;
            this.WaitThen(imageFader.fadeInTime + pauseTime, () => { ready.allowSceneActivation = true; });
        }
        else {
            SceneManager.LoadScene(idx, LoadSceneMode.Single);
        }
    }

    public void LoadFromPath(string scenePath) {
        _isLoading = true;
        imageFader.FadeIn();
        Lil.Guy.FadeMusicOut();
        var ready = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Single);
        ready.allowSceneActivation = false;
        this.WaitThen(imageFader.fadeInTime + pauseTime, () => { ready.allowSceneActivation = true; });
    }
}