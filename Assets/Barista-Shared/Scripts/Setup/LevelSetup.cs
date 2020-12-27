using System.Collections.Generic;

namespace Barista.Shared.Configuration
{
    public class LevelSetup
    {
        public EnvironmentSetup EnvironmentSetup { get; }
        public HeroSetup HeroSetup { get; }
        public IReadOnlyList<EnemySetup> EnemySetups { get; }

        public LevelSetup(
            EnvironmentSetup environmentSetup,
            HeroSetup heroSetup,
            IReadOnlyList<EnemySetup> enemySetups
            )
        {
            EnvironmentSetup = environmentSetup;
            HeroSetup = heroSetup;
            EnemySetups = enemySetups;
        }
    }
}