using Juce.Core.Architecture;
using Juce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Barista.Client.View.Entities.Environment
{
    public class EnvironmentEntityViewRepository : IRepository<EnvironmentEntityView>
    {
        private readonly Dictionary<int, EnvironmentEntityView> elements = new Dictionary<int, EnvironmentEntityView>();

        private readonly IEnvironmentEntityViewFactory environmentEntityViewFactory;

        public EnvironmentEntityViewRepository(IEnvironmentEntityViewFactory environmentEntityViewFactory)
        {
            this.environmentEntityViewFactory = environmentEntityViewFactory;
        }

        public EnvironmentEntityView Spawn(string typeId, int instanceId)
        {
            EnvironmentEntityView environmentEntityView = environmentEntityViewFactory.Create(typeId, instanceId);

            elements.Add(environmentEntityView.InstanceId, environmentEntityView);

            return environmentEntityView;
        }

        public void Despawn(int instanceId)
        {
            EnvironmentEntityView toDespawn = Get(instanceId);

            environmentEntityViewFactory.Destroy(toDespawn);

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

        public EnvironmentEntityView Get(int instanceId)
        {
            bool found = elements.TryGetValue(instanceId, out EnvironmentEntityView environmentEntityView);

            Contract.IsTrue(found);

            return environmentEntityView;
        }

        public Lazy<EnvironmentEntityView> GetLazy(int instanceId)
        {
            return new Lazy<EnvironmentEntityView>(() => { return Get(instanceId); });
        }
    }
}