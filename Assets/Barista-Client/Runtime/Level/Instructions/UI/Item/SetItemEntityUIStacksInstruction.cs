using Barista.Client.UI.Items;
using Barista.Shared.Logic.Items;
using Juce.Core.Sequencing;

namespace Barista.Client.Level.Instructions.UI.Item
{
    public class SetItemEntityUIStacksInstruction : InstantInstruction
    {
        private readonly ItemsUIView itemsUIView;
        private readonly ItemType itemType;
        private readonly int itemStacks;

        public SetItemEntityUIStacksInstruction(
            ItemsUIView itemsUIView,
            ItemType itemType,
            int itemStacks
            )
        {
            this.itemsUIView = itemsUIView;
            this.itemType = itemType;
            this.itemStacks = itemStacks;
        }

        protected override void OnInstantExecute()
        {
            itemsUIView.SetItemStacks(itemType, itemStacks);
        }
    }
}