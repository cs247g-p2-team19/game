using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WordHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent<Word> onOverlap;
    public UnityEvent<Word> onExitOverlap;
    private void OnTriggerEnter2D(Collider2D other) {
        Word w = other.GetComponentInParent<Word>();
        if (w != null) {
            onOverlap.Invoke((Word) w);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Word w = other.GetComponentInParent<Word>();
        if (w != null) {
            onExitOverlap.Invoke((Word) w);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
