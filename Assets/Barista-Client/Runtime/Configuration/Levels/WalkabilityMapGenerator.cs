using Barista.Client.View.Entities.Environment;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Tools.Configuration
{
    public static class WalkabilityMapGenerator
    {
        public static IReadOnlyList<Vector2Int> Generate(EnvironmentEntityView environmentEntityView)
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
    }
}
