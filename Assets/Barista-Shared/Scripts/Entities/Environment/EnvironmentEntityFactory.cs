using Barista.Shared.Configuration;
using Juce.Core.Id;

namespace Barista.Shared.Entities.Environment
{
    public class EnvironmentEntityFactory
    {
        private readonly IIdGenerator idGenerator;

        public EnvironmentEntityFactory(IIdGenerator idGenerator)
        {
            this.idGenerator = idGenerator;
        }

        public EnvironmentEntity Create(EnvironmentSetup environmentConfiguration)
        {
            EnvironmentEntity environmentEntity = new EnvironmentEntity(environmentConfiguration.TypeId, idGenerator.Generate());

            return environmentEntity;
        }
    }
}