using Barista.Shared.Configuration;
using Juce.Core.Id;

namespace Barista.Shared.Entities.Environment
{
    public class HeroEntityFactory
    {
        private readonly IIdGenerator idGenerator;

        public HeroEntityFactory(IIdGenerator idGenerator)
        {
            this.idGenerator = idGenerator;
        }

        public HeroEntity Create(HeroSetup heroConfiguration)
        {
            HeroEntity heroEntity = new HeroEntity(heroConfiguration.TypeId, idGenerator.Generate());

            return heroEntity;
        }
    }
}