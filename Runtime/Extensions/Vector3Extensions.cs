using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public static class Vector3Extensions
    {
        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }

        public static Vector3 WithZeroY(this Vector3 vector3)
        {
            return new Vector3(vector3.x, 0, vector3.z);
        }

        public static Vector3 WithZeroZ(this Vector3 vector3) =>
            new Vector3(vector3.x, vector3.y, 0);

        public static Vector3 WithZeroYZ(this Vector3 vector3) =>
            new Vector3(vector3.x, 0, 0);

        public static Vector3 WithZeroXZ(this Vector3 vector3) =>
            new Vector3(0, vector3.y, 0);

        public static Vector3 WithZeroXY(this Vector3 vector3) =>
            new Vector3(0, 0, vector3.z);

        public static Vector3 WithZeroX(this Vector3 vector3) =>
            new Vector3(0, vector3.y, vector3.z);

        public static Vector3 HadaProduct(this Vector3 a, Vector3 b) =>
            new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);

       
    }
}
