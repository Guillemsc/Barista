using Barista.Shared.Logic.Range;

namespace Barista.Shared.Logic.Items
{
    public interface IItem
    {
        ItemType Type { get; }
        ItemTargetType ItemTargetType { get; }
        int Stacks { get; }

        void AddStack();
        void RemoveStack();
    }
}
