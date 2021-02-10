using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.Items;
using Barista.Shared.Logic.Range;
using Barista.Shared.State;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Logic
{
    public class HeroItemEffectLogicAction : IHeroItemEffectLogicAction
    {
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly LevelState levelState;

        public HeroItemEffectLogicAction(
            HeroEntityRepository heroEntityRepository,
            LevelState levelState
            )
        {
            this.heroEntityRepository = heroEntityRepository;
            this.levelState = levelState;
        }

        public bool ItemEffectNeedsTarget(ItemType itemType)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            bool itemFound = heroEntity.Items.TryGetValue(itemType, out IItem item);

            if (!itemFound)
            {
                return false;
            }

            if(!item.EffectRange.Used)
            {
                return false;
            }

            return true;
        }

        public IReadOnlyList<Int2> GetItemAvaliableTargets(ItemType itemType)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            bool itemFound = heroEntity.Items.TryGetValue(itemType, out IItem item);

            if (!itemFound)
            {
                return null;
            }

            if (!item.EffectRange.Used)
            {
                return null;
            }

            List<Int2> positions = item.EffectRange.GetRangePositions(heroEntity.GridPosition);

            positions.Remove(heroEntity.GridPosition);

            return positions;
        }

        public void ApplyItemEffect(ItemType itemType, Int2 targetPosition)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            bool itemFound = heroEntity.Items.TryGetValue(itemType, out IItem item);

            if (!itemFound)
            {
                return;
            }

            item.Effect.Execute();
        }
    }
}
