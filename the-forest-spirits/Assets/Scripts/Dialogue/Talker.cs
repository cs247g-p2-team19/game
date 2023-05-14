using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Talker : MonoBehaviour
{
    public TextMeshPro textRef;
    public Branch startBranch;
    public UnityEvent onStart;
    public UnityEvent onEnd;
    public UnityEvent onNext;

    [SerializeField, ReadOnly] private int _index = 0;
    [SerializeField, ReadOnly] private bool _talking = false;
    [SerializeField, ReadOnly] private Conversation _currentConversation;

    public void OnInteract() {
        if (!_talking) {
            StartConversation();
        }
        else {
            Next();
        }
    }
    
    public void StartConversation() {
        _currentConversation = startBranch.GetConversation();
        _currentConversation.onStart.Invoke();
        if (_currentConversation == null) return;
        
        _talking = true;
        _index = -1;
        Setup();
        Next();
    }

    private void Setup() {
        onStart.Invoke();
    }

    private void Teardown() {
        onEnd.Invoke();
        _talking = false;
        _index = 0;
        _currentConversation = null;
        textRef.text = "";
    }

    private void Next() {
        if (_index >= 0) {
            _currentConversation.dialogue[_index].onEnd.Invoke();
        }
        _index++;


        if ((_index < _currentConversation.dialogue.Length)) {
            onNext.Invoke();
            _currentConversation.dialogue[_index].onStart.Invoke();
            textRef.text = _currentConversation.dialogue[_index].text;
            Debug.Log(_currentConversation.dialogue[_index].text);
            return;
        }
        
        _currentConversation.onEnd.Invoke();
        if (_currentConversation.andThen == null) {
            Teardown();
            return;
        }
        
        _currentConversation = _currentConversation.andThen.GetConversation();
        if (_currentConversation == null) {
            Teardown();
        }
        else {
            _index = -1;
            _currentConversation.onStart.Invoke();
            Next();
        }
    }
}