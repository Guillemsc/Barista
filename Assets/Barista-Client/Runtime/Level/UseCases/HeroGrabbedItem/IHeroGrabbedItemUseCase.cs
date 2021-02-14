using Barista.Shared.Logic.Items;
using Juce.Core.Sequencing;

namespace Barista.Client.Level.UseCases
{
    public interface IHeroGrabbedItemUseCase
    {
        Instruction Invoke(
            int heroEntityInstanceId, 
            int itemEntityInstanceId,
            ItemType itemType,
            int totalStacks
            );
    }
}
