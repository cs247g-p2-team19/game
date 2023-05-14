using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoorOpen() {
        animator.SetBool("Open", true);
    }

    public void DoorClose() {
        animator.SetBool("Open", false);
    }
}
