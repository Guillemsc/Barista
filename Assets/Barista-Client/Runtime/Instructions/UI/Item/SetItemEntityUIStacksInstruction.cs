using Barista.Client.UI.Items;
using Barista.Shared.Logic.Items;
using Juce.Core.Sequencing;

namespace Barista.Client.Instructions.UI.Item
{
    public class SetItemEntityUIStacksInstruction : InstantInstruction
    {
        private readonly ItemsViewUI itemsViewUI;
        private readonly ItemType itemType;
        private readonly int itemStacks;

        public SetItemEntityUIStacksInstruction(
            ItemsViewUI itemsViewUI,
            ItemType itemType,
            int itemStacks
            )
        {
            this.itemsViewUI = itemsViewUI;
            this.itemType = itemType;
            this.itemStacks = itemStacks;
        }

        protected override void OnInstantStart()
        {
            itemsViewUI.SetItemStacks(itemType, itemStacks);
        }
    }
}