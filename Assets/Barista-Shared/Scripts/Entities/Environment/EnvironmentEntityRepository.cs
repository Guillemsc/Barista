using Barista.Shared.Configuration;
using Juce.Core.Architecture;
using Juce.Core.Contracts;
using System.Collections.Generic;

namespace Barista.Shared.Entities.Environment
{
    public class EnvironmentEntityRepository : IRepository<EnvironmentEntity>
    {
        private readonly Dictionary<int, EnvironmentEntity> elements = new Dictionary<int, EnvironmentEntity>();

        private readonly EnvironmentEntityFactory environmentEntityFactory;

        public EnvironmentEntityRepository(EnvironmentEntityFactory environmentEntityFactory)
        {
            this.environmentEntityFactory = environmentEntityFactory;
        }

        public EnvironmentEntity Spawn(EnvironmentConfiguration environmentConfiguration)
        {
            EnvironmentEntity environmentEntity = environmentEntityFactory.Create(environmentConfiguration);

            elements.Add(environmentEntity.InstanceId, environmentEntity);

            return environmentEntity;
        }

        public EnvironmentEntity Get(int id)
        {
            bool found = elements.TryGetValue(id, out EnvironmentEntity environmentEntity);

            Contract.IsTrue(found);

            return environmentEntity;
        }
    }
}