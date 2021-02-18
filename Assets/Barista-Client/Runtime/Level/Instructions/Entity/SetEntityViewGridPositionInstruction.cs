using Barista.Client.Utils;
using Barista.Client.View.Entities;
using Barista.Client.View.Entities.Environment;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System;
using UnityEngine;

namespace Barista.Client.Level.Instructions.Entity
{
    public class SetEntityViewGridPositionInstruction : InstantInstruction
    {
        private readonly Lazy<EnvironmentEntityView> environment;
        private readonly Lazy<IMovableEntityView> movableEntityView;
        private readonly Int2 gridPosition;

        public SetEntityViewGridPositionInstruction(
            Lazy<EnvironmentEntityView> environment,
            Lazy<IMovableEntityView> movableEntityView,
            Int2 gridPosition
            )
        {
            this.environment = environment;
            this.movableEntityView = movableEntityView;
            this.gridPosition = gridPosition;
        }

        protected override void OnInstantExecute()
        {
            Vector3Int gridPositionVector = TilemapUtils.Int2ToVector3(gridPosition);

            Vector3 positionVector = environment.Value.WalkabilityTilemap.GetCellCenterWorld(gridPositionVector);

            movableEntityView.Value.Transform.position = positionVector;
        }
    }
}