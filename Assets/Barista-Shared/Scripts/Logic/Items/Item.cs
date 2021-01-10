using System;

namespace Barista.Shared.Logic.Items
{
    public class Item : IItem
    {
        public ItemType Type { get; }

        public int Stacks { get; private set; } 

        public Item(ItemType type)
        {
            Type = type;
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
