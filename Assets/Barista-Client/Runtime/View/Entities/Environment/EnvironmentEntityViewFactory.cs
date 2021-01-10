using Barista.Client.Libraries;

namespace Barista.Client.View.Entities.Environment
{
    public class EnvironmentEntityViewFactory : IEnvironmentEntityViewFactory
    {
        private readonly EnvironmentsLibrary environmentsLibrary;

        public EnvironmentEntityViewFactory(EnvironmentsLibrary environmentsLibrary)
        {
            this.environmentsLibrary = environmentsLibrary;
        }

        public EnvironmentEntityView Create(string typeId, int instanceId)
        {
            bool found = environmentsLibrary.TryGetItem(typeId, out EnvironmentEntityView environmentEntityView);

            if (!found)
            {
                throw new System.Exception($"{nameof(EnvironmentEntityView)} of type {typeId} could not " +
                    $"be found on {nameof(EnvironmentEntityViewFactory)}");
            }

            if (environmentEntityView == null)
            {
                throw new System.Exception($"{nameof(EnvironmentEntityView)} of type '{typeId}' and instance " +
                    $"{instanceId} was null on {nameof(EnvironmentEntityViewFactory)}");
            }

            EnvironmentEntityView newEnvironmentEntityView = environmentEntityView.InstantiateGameObjectAndGet();

            newEnvironmentEntityView.Construct(typeId, instanceId);

            return newEnvironmentEntityView;
        }

        public void Destroy(EnvironmentEntityView toDestroy)
        {
            toDestroy.CleanUp();

            toDestroy.DestroyGameObject();
        }
    }
}