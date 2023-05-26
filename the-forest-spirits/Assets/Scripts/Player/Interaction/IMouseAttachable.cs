using System.Collections.Generic;
using UnityEngine;

public interface IMouseAttachable
{
    public bool OnTryAttach(MouseManager manager);
    public bool OnClickWhileAttached(List<IMouseEventReceiver> others, MouseManager manager);
}