﻿using Juce.Core.Containers;
using Juce.Core.Pathfinding;
using Juce.Core.Pathfinding.Algorithms;
using System.Collections.Generic;
using System.Linq;

namespace Barista.Shared.Logic.Pathfinding
{
    public class ExpansionFactory
    {
        private readonly EnvironmentPathfindingUtils pathfindingUtils;

        private Int2 origin;
        private int range;

        public ExpansionFactory(EnvironmentPathfindingUtils pathfindingUtils)
        {
            this.pathfindingUtils = pathfindingUtils;
        }

        public List<Int2> Expand(Int2 origin, int range)
        {
            this.origin = origin;
            this.range = range;

            BFSPathfindingAlgorithm<Int2> algorithm = new BFSPathfindingAlgorithm<Int2>(
                GetChilds,
                GenerateResult,
                origin
                );

            algorithm.Start();

            while (!algorithm.Finished)
            {
                algorithm.Update();
            }

            return algorithm.Result;
        }

        private IReadOnlyList<Int2> GetChilds(Int2 parent)
        {
            List<Int2> ret = new List<Int2>();

            List<Int2> toCheck = new List<Int2>(4);

            toCheck.Add(new Int2(parent.X, parent.Y + 1));
            toCheck.Add(new Int2(parent.X, parent.Y - 1));
            toCheck.Add(new Int2(parent.X - 1, parent.Y));
            toCheck.Add(new Int2(parent.X + 1, parent.Y));

            foreach (Int2 positionToCheck in toCheck)
            {
                if (pathfindingUtils.InsideRange(origin, positionToCheck, range))
                {
                    if (pathfindingUtils.WalkabilityGridContains(positionToCheck))
                    {
                        if (!pathfindingUtils.IsUsedByEntity(positionToCheck))
                        {
                            ret.Add(positionToCheck);
                        }
                    }
                }
            }

            return ret;
        }

        private List<Int2> GenerateResult(IReadOnlyDictionary<Int2, PathfindingNode<Int2>> nodes)
        {
            return nodes.Keys.ToList();
        }
    }
}
