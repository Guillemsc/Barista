using Juce.Core.Containers;
using System;

namespace Barista.Client.Signals
{
    public class TargetSelectorSelectedSignal
    {
        private Action<Int2> onTriggered;

        public void Trigger(Int2 gridPosition)
        {
            onTriggered?.Invoke(gridPosition);
        }

        public void Register(Action<Int2> register)
        {
            onTriggered += register;
        }

        public void CleanUp()
        {
            onTriggered = null;
        }
    }
}
