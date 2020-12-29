using Barista.Client.Utils;
using Barista.Client.View.Entities;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using Juce.Tween;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Barista.Client.Instructions.Entity
{
    public class MoveEntityViewAlongPathInstruction : AsyncInstruction
    {
        private readonly Lazy<EnvironmentEntityView> environmentEntityView;
        private readonly Lazy<IMovableEntityView> movableEntityView;
        private readonly IReadOnlyList<Int2> path;

        public MoveEntityViewAlongPathInstruction(
            Lazy<EnvironmentEntityView> environmentEntityView,
            Lazy<IMovableEntityView> movableEntityView,
            IReadOnlyList<Int2> path
            )
        {
            this.environmentEntityView = environmentEntityView;
            this.movableEntityView = movableEntityView;
            this.path = path;
        }

        protected override Task OnAsyncStart()
        {
            TaskCompletionSource<bool> taskCompletitionSource = new TaskCompletionSource<bool>();

            SequenceTween sequence = new SequenceTween();

            for (int i = 0; i < path.Count; ++i)
            {
                if (i == 0)
                {
                    continue;
                }

                Int2 currGridPos = path[i];

                Vector3 worldPosition = environmentEntityView.Value.WalkabilityTilemap.
                    GetCellCenterWorld(TilemapUtils.Int2ToVector3(currGridPos));

                sequence.Append(movableEntityView.Value.Transform.TweenPosition(worldPosition, 0.2f));
            }

            sequence.SetEase(Ease.InOutQuad);

            sequence.onCompleteOrKill += () => taskCompletitionSource.SetResult(true);

            sequence.Play();

            return taskCompletitionSource.Task;
        }
    }
}