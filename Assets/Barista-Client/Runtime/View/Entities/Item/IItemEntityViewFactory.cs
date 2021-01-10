using Barista.Shared.Logic.Items;
using System;

namespace Barista.Client.View.Entities.Item
{
    public interface IItemEntityViewFactory
    {
        ItemEntityView Create(ItemType typeId, int instanceId);

        void Destroy(ItemEntityView toDestroy);
    }
}