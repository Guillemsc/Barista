using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;

namespace Barista.Client.Actions
{
    public interface ISetupLevelAction
    {
        void Invoke(
            EnvironmentEntity environmentEntity,
            HeroEntity heroEntity
            );
    }
}