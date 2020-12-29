using System;

namespace Barista.Client.View.Entities.Item
{
    public interface IItemEntityViewFactory
    {
        ItemEntityView Create(string typeId, int instanceId);

        void Destroy(ItemEntityView toDestroy);
    }
}