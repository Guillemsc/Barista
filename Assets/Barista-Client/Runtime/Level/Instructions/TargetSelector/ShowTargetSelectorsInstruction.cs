using Barista.Client.Utils;
using Barista.Client.View.Effects.TargetSelector;
using Barista.Client.View.Entities.Environment;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Level.Instructions.TargetSelector
{
    public class ShowTargetSelectorsInstruction : InstantInstruction
    {
        private readonly TargetSelectorViewRepository targetSelectorViewRepository;
        private readonly Lazy<EnvironmentEntityView> environmentView;
        private readonly IReadOnlyList<Int2> gridPositions;

        public ShowTargetSelectorsInstruction(
            TargetSelectorViewRepository targetSelectorViewRepository,
            Lazy<EnvironmentEntityView> environmentView,
            IReadOnlyList<Int2> gridPositions
            )
        {
            this.targetSelectorViewRepository = targetSelectorViewRepository;
            this.environmentView = environmentView;
            this.gridPositions = gridPositions;
        }

        protected override void OnInstantExecute()
        {
            targetSelectorViewRepository.DespawnAll();
            targetSelectorViewRepository.Spawn(gridPositions);

            foreach(KeyValuePair<Int2, TargetSelectorView> targetSelector in targetSelectorViewRepository.Elements)
            {
                Vector3Int gridPositionVector = TilemapUtils.Int2ToVector3(targetSelector.Key);

                Vector3 positionVector = environmentView.Value.WalkabilityTilemap.GetCellCenterWorld(gridPositionVector);

                targetSelector.Value.gameObject.transform.position = positionVector;

                targetSelector.Value.Show();
            }
        }
    }
}