using Barista.Shared.Entities.Enemy;
using Juce.Core.Containers;


namespace Barista.Shared.Logic.Utils
{
    public static class EnemiesUtils
    {
        public static bool TryGetEnemyAtPosition(
            EnemyEntityRepository enemyEntityRepository, 
            Int2 position, 
            out EnemyEntity enemyEntity
            )
        {
            foreach(EnemyEntity entity in enemyEntityRepository.Elements)
            {
                if(entity.GridPosition.Equals(position))
                {
                    enemyEntity = entity;
                    return true;
                }
            }

            enemyEntity = null;
            return false;
        }
    }
}
