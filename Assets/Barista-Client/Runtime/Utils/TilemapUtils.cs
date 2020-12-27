using Juce.Core.Containers;
using UnityEngine;

namespace Barista.Client.Utils
{
    public static class TilemapUtils
    {
        public static Int2 Vector2ToInt2(Vector2Int value)
        {
            return new Int2(value.x, value.y);
        }

        public static Vector2Int Int2ToVector2(Int2 value)
        {
            return new Vector2Int(value.X, value.Y);
        }

        public static Vector3Int Int2ToVector3(Int2 value)
        {
            return new Vector3Int(value.X, value.Y, 0);
        }
    }
}
