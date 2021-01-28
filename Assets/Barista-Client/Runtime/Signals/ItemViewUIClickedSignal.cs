using Barista.Shared.Logic.Items;
using System;

namespace Barista.Client.Signals
{
    public class ItemViewUIClickedSignal
    {
        private Action<ItemType> onTriggered;

        public void Trigger(ItemType itemType)
        {
            onTriggered?.Invoke(itemType);
        }

        public void Register(Action<ItemType> register)
        {
            onTriggered += register;
        }

        public void CleanUp()
        {
            onTriggered = null;
        }
    }
}
