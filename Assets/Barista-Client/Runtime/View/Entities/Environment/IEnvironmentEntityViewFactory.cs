using System;

namespace Barista.Client.View.Entities.Environment
{
    public interface IEnvironmentEntityViewFactory
    {
        EnvironmentEntityView Create(string typeId, int instanceId);

        void Destroy(EnvironmentEntityView toDestroy);
    }
}