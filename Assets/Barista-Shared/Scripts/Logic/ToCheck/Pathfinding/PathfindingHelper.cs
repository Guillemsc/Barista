using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Hero;
using Juce.Core.Containers;
using System;
using System.Collections.Generic;

namespace Juce.Core.Pathfinding.Algorithms
{
    public class EnvironmentPathfindingUtils
    {
        private readonly IReadOnlyList<Int2> walkabilityGrid;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly EnemyEntityRepository enemyEntityRepository;

        public EnvironmentPathfindingUtils(
           IReadOnlyList<Int2> walkabilityGrid,
           HeroEntityRepository heroEntityRepository,
           EnemyEntityRepository enemyEntityRepository
           )
        {
            this.walkabilityGrid = walkabilityGrid;
            this.heroEntityRepository = heroEntityRepository;
            this.enemyEntityRepository = enemyEntityRepository;
        }

        public bool WalkabilityGridContains(Int2 position)
        {
            foreach (Int2 walkablePosition in walkabilityGrid)
            {
                if (walkablePosition.Equals(position))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsUsedByEntity(Int2 position)
        {
            foreach (HeroEntity heroEntity in heroEntityRepository.Elements)
            {
                if (heroEntity.GridPosition.Equals(position))
                {
                    return true;
                }
            }

            foreach (EnemyEntity enemyEntity in enemyEntityRepository.Elements)
            {
                if (enemyEntity.GridPosition.Equals(position))
                {
                    return true;
                }
            }

            return false;
        }

        public bool InsideRange(Int2 position, Int2 toCheck, int range)
        {
            Int2 distance = position.ManhattanDistance(toCheck);

            distance.MakeAbs();

            if(distance.X > range || distance.Y > range)
            {
                return false;
            }

            return true;
        }
    }
}
