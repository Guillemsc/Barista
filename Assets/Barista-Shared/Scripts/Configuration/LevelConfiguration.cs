using System.Collections.Generic;

namespace Barista.Shared.Configuration
{
    public class LevelConfiguration
    {
        public EnvironmentConfiguration EnvironmentConfiguration { get; }

        public LevelConfiguration(
            EnvironmentConfiguration environmentConfiguration
            )
        {
            EnvironmentConfiguration = environmentConfiguration;
        }
    }
}