using Barista.Client.Level.Instructions.Item;
using Barista.Client.Level.Instructions.UI.Item;
using Barista.Client.UI.Items;
using Barista.Client.View.Entities.Item;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Juce.Core.Sequencing;

namespace Barista.Client.Actions
{
    //public class HeroGrabbedItemAction : IHeroGrabbedItemAction
    //{
    //    private readonly LevelTimelines levelTimelines;
    //    private readonly ItemEntityViewRepository itemEntityViewRepository;
    //    private readonly ItemsUIView itemsViewUI;

    //    public HeroGrabbedItemAction(
    //        LevelTimelines levelTimelines,
    //        ItemEntityViewRepository itemEntityViewRepository,
    //        ItemsUIView itemsViewUI
    //        )
    //    {
    //        this.levelTimelines = levelTimelines;
    //        this.itemEntityViewRepository = itemEntityViewRepository;
    //        this.itemsViewUI = itemsViewUI;
    //    }

    //    public void Invoke(
    //        HeroEntity heroEntity,
    //        ItemEntity itemEntity,
    //        int totalStacks
    //        )
    //    {
    //        InstructionsSequence sequence = new InstructionsSequence();

    //        sequence.Append(new DespawnItemEntityViewInstruction(
    //            itemEntityViewRepository,
    //            itemEntity.InstanceId
    //            ));

    //        sequence.Append(new SetItemEntityUIStacksInstruction(
    //            itemsViewUI,
    //            itemEntity.TypeId,
    //            totalStacks + 1
    //            ));

    //        levelTimelines.MainTimeline.Play(sequence);
    //    }
    //}
}