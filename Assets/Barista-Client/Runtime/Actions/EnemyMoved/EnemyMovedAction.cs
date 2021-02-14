using Barista.Client.Level.Instructions.Entity;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Barista.Shared.Entities.Enemy;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public class EnemyMovedAction 
    {
        //private readonly LevelTimelines levelTimelines;
        //private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        //private readonly EnemyEntityViewRepository enemyEntityViewRepository;

        //public EnemyMovedAction(
        //    LevelTimelines levelTimelines,
        //    EnvironmentEntityViewRepository environmentEntityViewRepository,
        //    EnemyEntityViewRepository enemyEntityViewRepository
        //    )
        //{
        //    this.levelTimelines = levelTimelines;
        //    this.environmentEntityViewRepository = environmentEntityViewRepository;
        //    this.enemyEntityViewRepository = enemyEntityViewRepository;
        //}

        //public void Invoke(EnemyEntity enemyEntity, IReadOnlyList<Int2> path)
        //{
        //    InstructionsSequence sequence = new InstructionsSequence();

        //    sequence.Append(new MoveEntityViewAlongPathInstruction(
        //        environmentEntityViewRepository.LoadedEnvironmentLazy,
        //        enemyEntityViewRepository.GetLazyAsMovable(enemyEntity.InstanceId),
        //        path
        //        ));

        //    levelTimelines.MainTimeline.Play(sequence);
        //}
    }
}