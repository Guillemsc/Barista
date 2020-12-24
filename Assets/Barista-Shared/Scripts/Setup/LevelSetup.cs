using System.Collections.Generic;

namespace Barista.Shared.Configuration
{
    public class LevelSetup
    {
        public EnvironmentSetup EnvironmentConfiguration { get; }
        public HeroSetup HeroConfiguration { get; }

        public LevelSetup(
            EnvironmentSetup environmentConfiguration,
            HeroSetup heroConfiguration
            )
        {
            EnvironmentConfiguration = environmentConfiguration;
            HeroConfiguration = heroConfiguration;
        }
    }
}