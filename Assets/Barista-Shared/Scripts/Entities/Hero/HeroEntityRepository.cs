using Barista.Shared.Configuration;
using Juce.Core.Architecture;
using Juce.Utils.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Barista.Shared.Entities.Hero
{
    public class HeroEntityRepository : IRepository<HeroEntity>
    {
        private readonly Dictionary<int, HeroEntity> elements = new Dictionary<int, HeroEntity>();

        private readonly HeroEntityFactory heroEntityFactory;

        public IReadOnlyList<HeroEntity> Elements => elements.Values.ToList();

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