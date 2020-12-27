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

        public EnvironmentEntity Create(EnvironmentSetup environmentSetup)
        {
            EnvironmentEntity environmentEntity = new EnvironmentEntity(environmentSetup.TypeId, idGenerator.Generate());

            return environmentEntity;
        }
    }
}