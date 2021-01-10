using System;

namespace Barista.Shared.Logic.Items
{
    public class ItemFactory
    {
        public IItem Create(ItemType type)
        {
            return new Item(type);
        }
    }
}
