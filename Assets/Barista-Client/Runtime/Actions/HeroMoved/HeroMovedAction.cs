using Barista.Client.Instructions.Entity;
using Barista.Client.Timelines;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public class HeroMovedAction : IHeroMovedAction
    {
        private readonly LevelTimelines levelTimelines;
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly HeroEntityViewRepository heroEntityViewRepository;

        public HeroMovedAction(
            LevelTimelines levelTimelines,
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            HeroEntityViewRepository heroEntityViewRepository
            )
        {
            this.levelTimelines = levelTimelines;
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.heroEntityViewRepository = heroEntityViewRepository;
        }

        public void Invoke(EnvironmentEntity environmentEntity, HeroEntity heroEntity, IReadOnlyList<Int2> path)
        {
            InstructionsSequence sequence = new InstructionsSequence();

            sequence.Append(new MoveEntityViewAlongPathInstruction(
                environmentEntityViewRepository.LoadedEnvironmentLazy,
                heroEntityViewRepository.GetLazyAsMovable(heroEntity.InstanceId),
                path
                ));

            levelTimelines.MainTimeline.Play(sequence);
        }
    }
}