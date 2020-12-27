using Barista.Client.Utils;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Barista.Client.Instructions.Hero
{
    public class SetHeroEntityViewGridPositionInstruction : InstantInstruction
    {
        private readonly Lazy<EnvironmentEntityView> environment;
        private readonly Lazy<HeroEntityView> heroEntityView;
        private readonly Int2 gridPosition;

        public SetHeroEntityViewGridPositionInstruction(
            Lazy<EnvironmentEntityView> environment,
            Lazy<HeroEntityView> heroEntityView,
            Int2 gridPosition
            )
        {
            this.environment = environment;
            this.heroEntityView = heroEntityView;
            this.gridPosition = gridPosition;
        }

        protected override void OnInstantStart()
        {
            Vector3Int gridPositionVector = TilemapUtils.Int2ToVector3(gridPosition);

            Vector3 positionVector = environment.Value.WalkabilityTilemap.GetCellCenterWorld(gridPositionVector);

            heroEntityView.Value.transform.position = positionVector;
        }
    }
}