using Barista.Shared.Entities.Environment;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public interface ILoadLevelAction
    {
        void Invoke(
            EnvironmentEntity environmentEntity
            );
    }
}