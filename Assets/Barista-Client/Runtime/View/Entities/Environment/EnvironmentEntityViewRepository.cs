using System;

namespace Barista.Client.View.Entities.Environment
{
    public class EnvironmentEntityViewRepository 
    {
        private readonly IEnvironmentEntityViewFactory environmentEntityViewFactory;

        private EnvironmentEntityView loadedEnvironment;

        public EnvironmentEntityView LoadedEnvironment => loadedEnvironment;

        public Lazy<EnvironmentEntityView> LoadedEnvironmentLazy => new Lazy<EnvironmentEntityView>(() => { return LoadedEnvironment; });

        public EnvironmentEntityViewRepository(IEnvironmentEntityViewFactory environmentEntityViewFactory)
        {
            this.environmentEntityViewFactory = environmentEntityViewFactory;
        }

        public void Spawn(string typeId, int instanceId)
        {
            loadedEnvironment = environmentEntityViewFactory.Create(typeId, instanceId);
        }

        public void Despawn()
        {
            environmentEntityViewFactory.Destroy(loadedEnvironment);

            loadedEnvironment = null;
        }
    }
}