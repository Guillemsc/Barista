using Barista.Shared.Configuration;
using Juce.Core.Id;

namespace Barista.Shared.Entities.Hero
{
    public class HeroEntityFactory
    {
        private readonly IIdGenerator idGenerator;

        public HeroEntityFactory(IIdGenerator idGenerator)
        {
            this.idGenerator = idGenerator;
        }

        public HeroEntity Create(HeroSetup heroSetup)
        {
            HeroEntity heroEntity = new HeroEntity(heroSetup.TypeId, idGenerator.Generate());

            return heroEntity;
        }
    }
}