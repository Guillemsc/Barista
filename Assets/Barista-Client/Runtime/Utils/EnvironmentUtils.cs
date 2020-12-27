using Barista.Client.View.Entities.Environment;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Utils
{
    public static class EnvironmentUtils
    {
        public static List<Vector2Int> GenerateWalkability(EnvironmentEntityView environmentEntityView)
        {
            List<Vector2Int> ret = new List<Vector2Int>();

            if (environmentEntityView == null)
            {
                return ret;
            }

            if (environmentEntityView.WalkabilityTilemap == null)
            {
                return ret;
            }

            environmentEntityView.WalkabilityTilemap.CompressBounds();

            BoundsInt boundsInt = environmentEntityView.WalkabilityTilemap.cellBounds;

            for (int x = boundsInt.min.x; x < boundsInt.max.x; ++x)
            {
                for (int y = boundsInt.min.y; y < boundsInt.max.y; ++y)
                {
                    bool tileExists = environmentEntityView.WalkabilityTilemap.GetTile(new Vector3Int(x, y, 0)) != null;

                    if (!tileExists)
                    {
                        continue;
                    }

                    ret.Add(new Vector2Int(x, y));
                }
            }

            return ret;
        }

        public static Vector2Int GenerateHeroSpawnPosition(EnvironmentEntityView environmentEntityView)
        {
            if (environmentEntityView == null)
            {
                return Vector2Int.zero;
            }

            if (environmentEntityView.WalkabilityTilemap == null)
            {
                return Vector2Int.zero;
            }

            Vector3Int spawnCell = environmentEntityView.GridLayout.
                WorldToCell(environmentEntityView.HeroEntitySpawnView.transform.position);

            return new Vector2Int(spawnCell.x, spawnCell.y);
        }
    }
}
