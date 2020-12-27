using Barista.Client.Libraries;

namespace Barista.Client.View.Entities.Enemy
{
    public class EnemyEntityViewFactory : IEnemyEntityViewFactory
    {
        private readonly EnemiesLibrary enemiesLibrary;

        public EnemyEntityViewFactory(EnemiesLibrary enemiesLibrary)
        {
            this.enemiesLibrary = enemiesLibrary;
        }

        public EnemyEntityView Create(string typeId, int instanceId)
        {
            bool found = enemiesLibrary.TryGetItem(typeId, out EnemyEntityView enemyEntityView);

            if (!found)
            {
                throw new System.Exception($"{nameof(EnemyEntityView)} of type {typeId} could not " +
                    $"be found on {nameof(EnemyEntityViewFactory)}");
            }

            if (enemyEntityView == null)
            {
                throw new System.Exception($"{nameof(EnemyEntityView)} of type '{typeId}' and instance " +
                    $"{instanceId} was null on {nameof(EnemyEntityViewFactory)}");
            }

            EnemyEntityView newEnemyEntityView = enemyEntityView.gameObject.InstantiateAndGetComponent<EnemyEntityView>();

            newEnemyEntityView.Construct(typeId, instanceId);

            return newEnemyEntityView;
        }

        public void Destroy(EnemyEntityView toDestroy)
        {
            toDestroy.CleanUp();

            toDestroy.DestroyGameObject();
        }
    }
}