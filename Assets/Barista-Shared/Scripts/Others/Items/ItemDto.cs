using System.Collections.Generic;

namespace Barista.Shared.Logic.Items
{
    public class ItemDto
    {
        public ItemType Type { get; private set; }
        public ItemTargetType ItemTargetType { get; private set; }
        public int Stacks { get; private set; }

        public static ItemDto ToDto(IItem item)
        {
            return new ItemDto()
            {
                Type = item.Type,
                ItemTargetType = item.ItemTargetType,
                Stacks = item.Stacks,
            };
        }

        public static List<ItemDto> ToDto(IReadOnlyList<IItem> list)
        {
            List<ItemDto> ret = new List<ItemDto>();

            foreach (IItem entity in list)
            {
                ret.Add(ToDto(entity));
            }

            return ret;
        }
    }
}
