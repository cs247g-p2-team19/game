using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{

    public bool IsOpen => _animator.GetBool(Open);

    public int targetSceneId;
    
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void DoorOpen() {
        var sfx = SceneInfo.Instance.doorOpen;
        if (sfx != null) {
            Lil.Guy.PlaySFX(sfx);
        }
        _animator.SetBool(Open, true);
    }

    public void DoorClose() {
        var sfx = SceneInfo.Instance.doorClosed;
        if (sfx != null) {
            Lil.Guy.PlaySFX(sfx);
        }
        _animator.SetBool(Open, false);
    }

    public void TransitionScene() {
        SceneTransitioner.Instance.Load(targetSceneId);
    }
}
