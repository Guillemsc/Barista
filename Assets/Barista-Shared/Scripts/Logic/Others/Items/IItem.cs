using Barista.Shared.Logic.Range;

namespace Barista.Shared.Logic.Items
{
    public interface IItem
    {
        ItemType Type { get; }
        int Stacks { get; }
        IItemEffect Effect { get; }
        IRange EffectRange { get; }

        void AddStack();
        void RemoveStack();
    }
}
