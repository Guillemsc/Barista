using Barista.Client.Level.Instructions.Entity;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Barista.Client.Level.UseCases
{
    public class MoveEnemyUseCase : IMoveEnemyUseCase
    {
        private readonly Sequencer mainSequencer;
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly EnemyEntityViewRepository enemyEntityViewRepository;

        public MoveEnemyUseCase(
            Sequencer mainSequencer,
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            EnemyEntityViewRepository enemyEntityViewRepository
            )
        {
            this.mainSequencer = mainSequencer;
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.enemyEntityViewRepository = enemyEntityViewRepository;
        }

        public void Invoke(
            int enemyEntityInstanceId,
            IReadOnlyList<Int2> path
            )
        {
            mainSequencer.Play(ct => Execute(
                enemyEntityInstanceId,
                path,
                ct
                ));
        }

        private async Task Execute(
            int enemyEntityInstanceId,
            IReadOnlyList<Int2> path,
            CancellationToken cancellationToken
            )
        {
            await new MoveEntityViewAlongPathInstruction(
                environmentEntityViewRepository.LoadedEnvironmentLazy,
                enemyEntityViewRepository.GetLazyAsMovable(enemyEntityInstanceId),
                path
                ).Execute(cancellationToken);
        }
    }
}
