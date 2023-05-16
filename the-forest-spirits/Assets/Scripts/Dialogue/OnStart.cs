using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent callback;
    void Start()
    {
        callback.Invoke();
    }

    // Update is called once per frame
}
