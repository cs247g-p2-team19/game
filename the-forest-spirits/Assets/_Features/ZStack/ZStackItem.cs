    using UnityEngine;

    public class ZStackItem : AutoMonoBehaviour
    {
        [AutoDefaultInParents, Required]
        public ZStack stack;

        [ReadOnly]
        public string debugStackState;

        public void MoveToTop() {
            stack.MoveToTop(this);
        }
        public void MoveToBottom() {
            stack.MoveToBottom(this);
        }
    }