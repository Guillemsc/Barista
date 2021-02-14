using Barista.Client.View.Entities.Hero;
using Juce.Core.Sequencing;

namespace Barista.Client.Level.Instructions.Hero
{
    public class SpawnHeroEntityViewInstruction : InstantInstruction
    {
        private readonly HeroEntityViewRepository heroEntityViewRepository;
        private readonly string typeId;
        private readonly int instanceId;

        public SpawnHeroEntityViewInstruction(
            HeroEntityViewRepository heroEntityViewRepository,
            string typeId, 
            int instanceId
            )
        {
            this.heroEntityViewRepository = heroEntityViewRepository;
            this.typeId = typeId;
            this.instanceId = instanceId;
        }

        protected override void OnInstantStart()
        {
            heroEntityViewRepository.Spawn(typeId, instanceId);
        }
    }
}