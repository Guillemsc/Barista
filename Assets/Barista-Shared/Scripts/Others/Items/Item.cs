using Barista.Shared.Logic.Range;
using System;

namespace Barista.Shared.Logic.Items
{
    public abstract class Item : IItem
    {
        public ItemType Type { get; }
        public ItemTargetType ItemTargetType { get; }
        public int Stacks { get; private set; } 

        public Item(ItemType type, ItemTargetType itemTargetType)
        {
            Type = type;
            ItemTargetType = itemTargetType;
        }

        public void AddStack()
        {
            Stacks += 1;
        }

        public void RemoveStack()
        {
            Stacks = Math.Max(0, Stacks - 1);
        }
    }
}
