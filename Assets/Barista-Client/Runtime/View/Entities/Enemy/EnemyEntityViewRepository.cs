using Juce.Core.Architecture;
using Juce.Utils.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Barista.Client.View.Entities.Enemy
{
    public class EnemyEntityViewRepository : IRepository<EnemyEntityView>
    {
        private readonly Dictionary<int, EnemyEntityView> elements = new Dictionary<int, EnemyEntityView>();

        private readonly IEnemyEntityViewFactory enemyEntityViewFactory;

        public EnemyEntityViewRepository(IEnemyEntityViewFactory enemyEntityViewFactory)
        {
            this.enemyEntityViewFactory = enemyEntityViewFactory;
        }

        public EnemyEntityView Spawn(string typeId, int instanceId)
        {
            EnemyEntityView enemyEntityView = enemyEntityViewFactory.Create(typeId, instanceId);

            elements.Add(enemyEntityView.InstanceId, enemyEntityView);

            return enemyEntityView;
        }

        public void Despawn(int instanceId)
        {
            EnemyEntityView toDespawn = Get(instanceId);

            enemyEntityViewFactory.Destroy(toDespawn);

            elements.Remove(instanceId);
        }

        public void DespawnAll()
        {
            List<int> toDespawn = elements.Keys.ToList();

            foreach (int instanceId in toDespawn)
            {
                Despawn(instanceId);
            }
        }

        public EnemyEntityView Get(int instanceId)
        {
            bool found = elements.TryGetValue(instanceId, out EnemyEntityView enemyEntityView);

            Contract.IsTrue(found);

            return enemyEntityView;
        }

        public Lazy<EnemyEntityView> GetLazy(int instanceId)
        {
            return new Lazy<EnemyEntityView>(() => { return Get(instanceId); });
        }

        public Lazy<IMovableEntityView> GetLazyAsMovable(int instanceId)
        {
            return new Lazy<IMovableEntityView>(() => { return Get(instanceId); });
        }
    }
}