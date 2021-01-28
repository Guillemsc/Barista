using Barista.Shared.Logic.Range;
using System;

namespace Barista.Shared.Logic.Items
{
    public class Item : IItem
    {
        public ItemType Type { get; }
        public int Stacks { get; private set; } 
        public IItemEffect Effect { get; private set; }
        public IRange EffectRange { get; }

        public Item(ItemType type, IItemEffect effect, IRange effectRange)
        {
            Type = type;
            Effect = effect;
            EffectRange = effectRange;
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
