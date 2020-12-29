using Juce.CoreUnity.Architecture;
using UnityEngine;

namespace Barista.Client.View.Entities.Item
{
    public class ItemEntityView : MonoBehaviour, IEntityView<string>
    {
        public string TypeId { get; private set; }
        public int InstanceId { get; private set; }

        public void Construct(string typeId, int instanceId)
        {
            TypeId = typeId;
            InstanceId = instanceId;
        }

        public void CleanUp()
        {

        }
    }
}
