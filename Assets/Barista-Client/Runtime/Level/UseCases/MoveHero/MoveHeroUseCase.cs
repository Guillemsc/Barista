using Barista.Client.Level.Instructions.Entity;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Barista.Client.Level.UseCases
{
    public class MoveHeroUseCase : IMoveHeroUseCase
    {
        private readonly Sequencer mainSequencer;
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly HeroEntityViewRepository heroEntityViewRepository;

        public MoveHeroUseCase(
            Sequencer mainSequencer,
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            HeroEntityViewRepository heroEntityViewRepository
            )
        {
            this.mainSequencer = mainSequencer;
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.heroEntityViewRepository = heroEntityViewRepository;
        }

        public void Invoke(
            int heroEntityInstanceId,
            IReadOnlyList<Int2> path
            )
        {
            mainSequencer.Play(ct => Execute(
                heroEntityInstanceId, 
                path, 
                ct
                ));
        }

        private async Task Execute(
            int heroEntityInstanceId,
            IReadOnlyList<Int2> path,
            CancellationToken cancellationToken
            )
        {
            await new MoveEntityViewAlongPathInstruction(
                environmentEntityViewRepository.LoadedEnvironmentLazy,
                heroEntityViewRepository.GetLazyAsMovable(heroEntityInstanceId),
                path
                ).Execute(cancellationToken);
        }
    }
}
