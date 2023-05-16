using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** Orchestrates leaving a scene and going to another scene. */
public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance => FindObjectOfType<SceneTransitioner>();
    public FadeImage imageFader;

    public void Load(int idx) {
        imageFader.FadeIn();
        Lil.Guy.FadeMusicOut();
        var ready = SceneManager.LoadSceneAsync(idx, LoadSceneMode.Single);
        ready.allowSceneActivation = false;
        this.WaitThen(imageFader.fadeInTime + 0.1f, () => {
            ready.allowSceneActivation = true;
        });
    }
}
