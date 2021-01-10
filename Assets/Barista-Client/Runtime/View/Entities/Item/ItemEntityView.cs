using Barista.Shared.Logic.Items;
using Juce.CoreUnity.Architecture;
using UnityEngine;

namespace Barista.Client.View.Entities.Item
{
    public class ItemEntityView : MonoBehaviour, IEntityView<ItemType>, IMovableEntityView
    {
        public ItemType TypeId { get; private set; }
        public int InstanceId { get; private set; }

        public Transform Transform => gameObject.transform;

        public void Construct(ItemType typeId, int instanceId)
        {
            TypeId = typeId;
            InstanceId = instanceId;
        }

        public void CleanUp()
        {

        }
    }
}
