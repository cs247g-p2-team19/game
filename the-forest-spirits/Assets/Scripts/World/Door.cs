using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void DoorOpen() {
        _animator.SetBool(Open, !_animator.GetBool(Open));
    }

}
