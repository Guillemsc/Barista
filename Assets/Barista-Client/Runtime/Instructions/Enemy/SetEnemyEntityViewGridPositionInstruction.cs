using Barista.Client.Utils;
using Barista.Client.View.Entities.Enemy;
using Barista.Client.View.Entities.Environment;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System;
using UnityEngine;

namespace Barista.Client.Instructions.Enemy
{
    public class SetEnemyEntityViewGridPositionInstruction : InstantInstruction
    {
        private readonly Lazy<EnvironmentEntityView> environment;
        private readonly Lazy<EnemyEntityView> enemyEntityView;
        private readonly Int2 gridPosition;

        public SetEnemyEntityViewGridPositionInstruction(
            Lazy<EnvironmentEntityView> environment,
            Lazy<EnemyEntityView> enemyEntityView,
            Int2 gridPosition
            )
        {
            this.environment = environment;
            this.enemyEntityView = enemyEntityView;
            this.gridPosition = gridPosition;
        }

        protected override void OnInstantStart()
        {
            Vector3Int gridPositionVector = TilemapUtils.Int2ToVector3(gridPosition);

            Vector3 positionVector = environment.Value.WalkabilityTilemap.GetCellCenterWorld(gridPositionVector);

            enemyEntityView.Value.transform.position = positionVector;
        }
    }
}