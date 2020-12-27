using Barista.Shared.Configuration;
using Juce.Core.Architecture;
using Juce.Core.Contracts;
using System.Collections.Generic;

namespace Barista.Shared.Entities.Hero
{
    public class HeroEntityRepository : IRepository<HeroEntity>
    {
        private readonly Dictionary<int, HeroEntity> elements = new Dictionary<int, HeroEntity>();

        private readonly HeroEntityFactory heroEntityFactory;

        public HeroEntityRepository(HeroEntityFactory heroEntityFactory)
        {
            this.heroEntityFactory = heroEntityFactory;
        }

        public HeroEntity Spawn(HeroSetup heroSetup)
        {
            HeroEntity heroEntity = heroEntityFactory.Create(heroSetup);

            elements.Add(heroEntity.InstanceId, heroEntity);

            return heroEntity;
        }

        public HeroEntity Get(int id)
        {
            bool found = elements.TryGetValue(id, out HeroEntity heroEntity);

            Contract.IsTrue(found);

            return heroEntity;
        }
    }
}