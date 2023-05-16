using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

 public class MouseHoverHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
     private bool mouse_over = false;
     public UnityEvent onHover;
     public UnityEvent onHoverExit;
     void Update()
     {
         if (mouse_over)
         {
             Debug.Log("Mouse Over");
         }
     }
 
     public void OnPointerEnter(PointerEventData eventData)
     {
         mouse_over = true;
         Debug.Log("Mouse enter");
         onHover.Invoke();
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         mouse_over = false;
         onHoverExit.Invoke();
         Debug.Log("Mouse exit");
     }
 }