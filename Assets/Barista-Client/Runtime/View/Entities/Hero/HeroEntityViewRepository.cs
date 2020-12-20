using Juce.Core.Architecture;
using Juce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Barista.Client.View.Entities.Hero
{
    public class HeroEntityViewRepository : IRepository<HeroEntityView>
    {
        private readonly Dictionary<int, HeroEntityView> elements = new Dictionary<int, HeroEntityView>();

        private readonly IHeroEntityViewFactory heroEntityViewFactory;

        public HeroEntityViewRepository(IHeroEntityViewFactory heroEntityViewFactory)
        {
            this.heroEntityViewFactory = heroEntityViewFactory;
        }

        public HeroEntityView Spawn(string typeId, int instanceId)
        {
            HeroEntityView heroEntityView = heroEntityViewFactory.Create(typeId, instanceId);

            elements.Add(heroEntityView.InstanceId, heroEntityView);

            return heroEntityView;
        }

        public void Despawn(int instanceId)
        {
            HeroEntityView toDespawn = Get(instanceId);

            heroEntityViewFactory.Destroy(toDespawn);

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

        public HeroEntityView Get(int instanceId)
        {
            bool found = elements.TryGetValue(instanceId, out HeroEntityView heroEntityView);

            Contract.IsTrue(found);

            return heroEntityView;
        }

        public Lazy<HeroEntityView> GetLazy(int instanceId)
        {
            return new Lazy<HeroEntityView>(() => { return Get(instanceId); });
        }
    }
}