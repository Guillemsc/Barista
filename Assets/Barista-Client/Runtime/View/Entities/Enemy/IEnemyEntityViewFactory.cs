using System;

namespace Barista.Client.View.Entities.Enemy
{
    public interface IEnemyEntityViewFactory
    {
        EnemyEntityView Create(string typeId, int instanceId);

        void Destroy(EnemyEntityView toDestroy);
    }
}