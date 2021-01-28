using Juce.Core.Containers;
using Juce.Core.Pathfinding;
using Juce.Core.Pathfinding.Algorithms;
using System.Collections.Generic;

namespace Barista.Shared.Logic.Pathfinding
{
    public class PathFactory
    {
        private readonly EnvironmentPathfindingUtils pathfindingUtils;

        private Int2 destination;

        public PathFactory(EnvironmentPathfindingUtils pathfindingUtils)
        {
            this.pathfindingUtils = pathfindingUtils;
        }

        public IReadOnlyList<Int2> CreatePath(Int2 origin, Int2 destination)
        {
            this.destination = destination;

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

            List<Int2> toCheck = new List<Int2>(4);

            toCheck.Add(new Int2(parent.X, parent.Y + 1));
            toCheck.Add(new Int2(parent.X, parent.Y - 1));
            toCheck.Add(new Int2(parent.X - 1, parent.Y));
            toCheck.Add(new Int2(parent.X + 1, parent.Y));

            foreach(Int2 positionToCheck in toCheck)
            {
                if (pathfindingUtils.WalkabilityGridContains(positionToCheck))
                {
                    if (!pathfindingUtils.IsUsedByEntity(positionToCheck))
                    {
                        ret.Add(positionToCheck);
                    }
                }
            }

            return ret;
        }

        private float GetPriority(Int2 node)
        {
            return -node.Distance(destination);
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
    }
}
