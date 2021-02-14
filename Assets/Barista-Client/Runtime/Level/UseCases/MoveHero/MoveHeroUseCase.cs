using Barista.Client.Level.Instructions.Entity;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Level.UseCases
{
    public class MoveHeroUseCase : IMoveHeroUseCase
    {
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly HeroEntityViewRepository heroEntityViewRepository;

        public MoveHeroUseCase(
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            HeroEntityViewRepository heroEntityViewRepository
            )
        {
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.heroEntityViewRepository = heroEntityViewRepository;
        }

        public Instruction Move(
            int heroEntityInstanceId,
            IReadOnlyList<Int2> path
            )
        {
            InstructionsSequence sequence = new InstructionsSequence();

            sequence.Append(new MoveEntityViewAlongPathInstruction(
                environmentEntityViewRepository.LoadedEnvironmentLazy,
                heroEntityViewRepository.GetLazyAsMovable(heroEntityInstanceId),
                path
                ));

            return sequence;
        }
    }
}
