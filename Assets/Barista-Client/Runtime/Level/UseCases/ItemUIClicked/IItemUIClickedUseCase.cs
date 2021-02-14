using Barista.Shared.Logic.Items;

namespace Barista.Client.Level.UseCases
{
    public interface IItemUIClickedUseCase
    {
        void Invoke(ItemType itemType);
    }
}
