using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{

    public bool IsOpen => _animator.GetBool(Open);

    public string targetSceneId;
    
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void DoorOpen() {
        _animator.SetBool(Open, true);
    }

    public void DoorClose() {
        _animator.SetBool(Open, false);
    }

    public void TransitionScene() {
        SceneManager.LoadScene(targetSceneId, LoadSceneMode.Single);
    }
}
