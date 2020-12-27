using Barista.Client.Utils;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using Juce.Tween;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Barista.Client.Instructions.Hero
{
    public class MoveHeroEntityViewAlongPathInstruction : AsyncInstruction
    {
        private readonly Lazy<EnvironmentEntityView> environmentEntityView;
        private readonly Lazy<HeroEntityView> heroEntityView;
        private readonly IReadOnlyList<Int2> path;

        public MoveHeroEntityViewAlongPathInstruction(
            Lazy<EnvironmentEntityView> environmentEntityView,
            Lazy<HeroEntityView> heroEntityView,
            IReadOnlyList<Int2> path
            )
        {
            this.environmentEntityView = environmentEntityView;
            this.heroEntityView = heroEntityView;
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

                sequence.Append(heroEntityView.Value.transform.TweenPosition(worldPosition, 0.2f));
            }

            sequence.SetEase(Ease.InOutQuad);

            sequence.onCompleteOrKill += () => taskCompletitionSource.SetResult(true);

            sequence.Play();

            return taskCompletitionSource.Task;
        }
    }
}