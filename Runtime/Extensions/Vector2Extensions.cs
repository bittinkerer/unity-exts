using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public static class Vector2Extensions
    {
        public static bool IsClockwise(this Vector2 vectorA, Vector2 vectorB)
        {
            if (vectorA.y * vectorB.x > vectorA.x * vectorB.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Vector3 ToVector3(this Vector2 vector2) =>
            new Vector3(vector2.x, vector2.y, 0);

        public static Vector3 ToVector3(this Vector2 vector2, float zAxis) =>
            new Vector3(vector2.x, vector2.y, zAxis);

        public static Vector3 ToXZVector3(this Vector2 vector2) =>
            new Vector3(vector2.x, 0, vector2.y);

        public static Vector3 ToXYVector3(this Vector2 vector2) =>
            new Vector3(vector2.x, vector2.y, 0);

        public static Vector2 WithZeroY(this Vector2 vector2) =>
            new Vector2(vector2.x, 0);

        public static Vector3 ToYZVector3(this Vector2 vector2) =>
            new Vector3(0, vector2.y, vector2.x);



    }
}
