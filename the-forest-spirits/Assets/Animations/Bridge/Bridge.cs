using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : AutoMonoBehaviour
{
    // Start is called before the first frame update
    public bool IsOpen => animator.GetBool(Open);
    
    [AutoDefault, ReadOnly]
    public Animator animator;
    private static readonly int Open = Animator.StringToHash("Open");
    
    public void OpenBridge() {
        animator.SetBool(Open, true);
    }

    public void CloseBridge() {
        animator.SetBool(Open, false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
