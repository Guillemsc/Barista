using Barista.Shared.Configuration;
using Juce.Core.Id;

namespace Barista.Shared.Entities.Item
{
    public class ItemEntityFactory
    {
        private readonly IIdGenerator idGenerator;

        public ItemEntityFactory(IIdGenerator idGenerator)
        {
            this.idGenerator = idGenerator;
        }

        public ItemEntity Create(ItemSetup itemSetup)
        {
            ItemEntity itemEntity = new ItemEntity(itemSetup.TypeId, idGenerator.Generate());

            return itemEntity;
        }
    }
}