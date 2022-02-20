using System;
using UnityEngine;

namespace Apolysis.InteractableSystem.ToolTip
{
    public class InteractableToolTip : MonoBehaviour
    {
        public event EventHandler<OnHoverEventArgs> InFocus;
        public event EventHandler<OffHoverEventArgs> LostFocus;

        public void Focus(string text)
        {
            if (InFocus != null)
                InFocus(this, new OnHoverEventArgs(text));
        }
        public void NoFocus()
        {
            if (LostFocus != null)
                LostFocus(this, new OffHoverEventArgs());
        }
    }
    public class OnHoverEventArgs : EventArgs
    {
        public OnHoverEventArgs(string text)
        {
            Text = text;
        }
        public string Text { get; private set; }
    }

    public class OffHoverEventArgs : EventArgs
    {
        public OffHoverEventArgs()
        {

        }
    }
}
