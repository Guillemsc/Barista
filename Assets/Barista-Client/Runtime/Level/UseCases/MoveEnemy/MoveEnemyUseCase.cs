using Barista.Client.Level.Instructions.Entity;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Level.UseCases
{
    public class MoveEnemyUseCase : IMoveEnemyUseCase
    {
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly EnemyEntityViewRepository enemyEntityViewRepository;

        public MoveEnemyUseCase(
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            EnemyEntityViewRepository enemyEntityViewRepository
            )
        {
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.enemyEntityViewRepository = enemyEntityViewRepository;
        }

        public Instruction Move(
            int enemyEntityInstanceId,
            IReadOnlyList<Int2> path
            )
        {
            InstructionsSequence sequence = new InstructionsSequence();

            sequence.Append(new MoveEntityViewAlongPathInstruction(
                environmentEntityViewRepository.LoadedEnvironmentLazy,
                enemyEntityViewRepository.GetLazyAsMovable(enemyEntityInstanceId),
                path
                ));

            return sequence;
        }
    }
}
