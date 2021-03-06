﻿using Barista.Client.View.Entities.Enemy;
using Juce.Core.Sequencing;

namespace Barista.Client.Level.Instructions.Enemy
{
    public class SpawnEnemyEntityViewInstruction : InstantInstruction
    {
        private readonly EnemyEntityViewRepository enemyEntityViewRepository;
        private readonly string typeId;
        private readonly int instanceId;

        public SpawnEnemyEntityViewInstruction(
            EnemyEntityViewRepository enemyEntityViewRepository,
            string typeId,
            int instanceId
            )
        {
            this.enemyEntityViewRepository = enemyEntityViewRepository;
            this.typeId = typeId;
            this.instanceId = instanceId;
        }

        protected override void OnInstantExecute()
        {
            enemyEntityViewRepository.Spawn(typeId, instanceId);
        }
    }
}