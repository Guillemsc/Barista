using Barista.Shared.Logic.Items;
using Juce.Core.Architecture;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Entities.Hero
{
    public class HeroEntity : IEntity<string>, IMapEntity, IAttackableEntity
    {
        public string TypeId { get; }
        public int InstanceId { get; }

        public Int2 GridPosition { get; private set; }
        public Int2 LastGridPosition { get; private set; }

        public Dictionary<ItemType, IItem> Items { get; } = new Dictionary<ItemType, IItem>();

        public bool Alive => throw new System.NotImplementedException();

        public HeroEntity(string typeId, int instanceId)
        {
            TypeId = typeId;
            InstanceId = instanceId;
        }

        public void SetGridPosition(Int2 position)
        {
            LastGridPosition = GridPosition;
            GridPosition =position;
        }
    }
}