using Juce.Core.Containers;
using Juce.Core.Pathfinding;
using Juce.Core.Pathfinding.Algorithms;
using System;
using System.Collections.Generic;

namespace Barista.Shared.Factories
{
    public class PathfindingFactory
    {
        private readonly IReadOnlyList<Int2> walkabilityGrid;

        private Int2 origin;
        private Int2 destination;

        public PathfindingFactory(IReadOnlyList<Int2> walkabilityGrid)
        {
            this.walkabilityGrid = walkabilityGrid;
        }

        public IReadOnlyList<Int2> Create(Int2 origin, Int2 destination)
        {
            AStarPathfindingAlgorithm<Int2> algorithm = new AStarPathfindingAlgorithm<Int2>(
                GetChilds,
                GetPriority,
                GenerateResult,
                origin,
                destination
                );

            algorithm.Start();

            while (!algorithm.Finished)
            {
                algorithm.Update();
            }

            return algorithm.Result.Result;
        }

        private IReadOnlyList<Int2> GetChilds(Int2 parent)
        {
            List<Int2> ret = new List<Int2>();

            Int2 upDirection = new Int2(parent.X, parent.Y + 1);
            Int2 downDirection = new Int2(parent.X, parent.Y - 1);
            Int2 leftDirection = new Int2(parent.X - 1, parent.Y);
            Int2 rightDirection = new Int2(parent.X + 1, parent.Y);

            if(WalkabilityGridContains(upDirection))
            {
                ret.Add(upDirection);
            }

            if (WalkabilityGridContains(downDirection))
            {
                ret.Add(downDirection);
            }

            if (WalkabilityGridContains(leftDirection))
            {
                ret.Add(leftDirection);
            }

            if (WalkabilityGridContains(rightDirection))
            {
                ret.Add(rightDirection);
            }

            return ret;
        }

        private float GetPriority(Int2 node)
        {
            return 0;
        }

        private PathfindingPath<Int2> GenerateResult(AStarPathfindingResult resultType, PathfindingNode<Int2> endNode)
        {
            List<Int2> result = new List<Int2>();

            PathfindingNode<Int2> currentNode = endNode;

            while(currentNode != null)
            {
                result.Add(currentNode.Value);

                currentNode = currentNode.Parent;
            }

            result.Reverse();

            return new PathfindingPath<Int2>(PathfindingResultType.Complete, result);
        }

        private bool WalkabilityGridContains(Int2 position)
        {
            foreach(Int2 walkablePosition in walkabilityGrid)
            {
                if(walkablePosition.Equals(position))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
