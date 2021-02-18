using Barista.Shared.Logic.Items;

namespace Barista.Client.Level.UseCases
{
    public interface IHeroGrabbedItemUseCase
    {
        void Invoke(
            int heroEntityInstanceId,
            int itemEntityInstanceId,
            ItemType itemType,
            int totalStacks
            );
    }
}
