using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.Items;
using Barista.Shared.Logic.Range;
using Barista.Shared.Logic.Utils;
using Barista.Shared.State;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Logic
{
    public class HeroItemEffectLogicAction : IHeroItemEffectLogicAction
    {
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly EnemyEntityRepository enemyEntityRepository;
        private readonly LevelState levelState;

        public HeroItemEffectLogicAction(
            HeroEntityRepository heroEntityRepository,
            EnemyEntityRepository enemyEntityRepository,
            LevelState levelState
            )
        {
            this.heroEntityRepository = heroEntityRepository;
            this.enemyEntityRepository = enemyEntityRepository;
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

            switch(item.ItemTargetType)
            {
                case ItemTargetType.Enemy:
                 {
                     return true;
                 }
            }

            return false;
        }

        public IReadOnlyList<Int2> GetItemAvaliableTargets(ItemType itemType)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            bool itemFound = heroEntity.Items.TryGetValue(itemType, out IItem item);

            if (!itemFound)
            {
                return null;
            }

            switch (item)
            {
                case EnemyTargetItem enemyTargeting:
                    {
                        List<Int2> positions = enemyTargeting.EffectRange.GetRangePositions(heroEntity.GridPosition);

                        List<Int2> finalPositions = new List<Int2>();

                        foreach (Int2 position in positions)
                        {
                            bool found = EnemiesUtils.TryGetEnemyAtPosition(enemyEntityRepository, position, out _);

                            if(found)
                            {
                                finalPositions.Add(position);
                            }
                        }

                        finalPositions.Remove(heroEntity.GridPosition);

                        return finalPositions;
                    }
            }

            return null;

        }

        public void ApplyItemEffect(ItemType itemType, Int2 targetPosition)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            bool itemFound = heroEntity.Items.TryGetValue(itemType, out IItem item);

            if (!itemFound)
            {
                return;
            }

            switch (item)
            {
                case EnemyTargetItem enemyTargeting:
                    {
                        bool found = EnemiesUtils.TryGetEnemyAtPosition(enemyEntityRepository, targetPosition, out EnemyEntity enemyEntity);

                        if(!found)
                        {
                            return;
                        }

                        enemyTargeting.EnemyItemEffect.Execute(enemyEntity);
                    }
                    break;
            }
        }
    }
}
