using Barista.Shared.Logic.Items;

namespace Barista.Shared.Actions
{
    public interface IUseItemAction
    {
        void Invoke(ItemType itemType);
    }
}