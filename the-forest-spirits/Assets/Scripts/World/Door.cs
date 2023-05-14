using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{

    public bool IsOpen => _animator.GetBool(Open);
    
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void DoorOpen() {
        _animator.SetBool(Open, !IsOpen);
    }

}
