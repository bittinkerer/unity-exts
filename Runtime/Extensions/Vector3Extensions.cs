using UnityEngine;

namespace Packages.Estenis.UnityExts_ {
  public static class Vector3Extensions {
    public static Vector2 ToVector2( this Vector3 vector3 ) =>
        new( vector3.x, vector3.z );

    public static Vector3 WithZeroY( this Vector3 vector3 ) =>
        new( vector3.x, 0, vector3.z );

    public static Vector3 WithZeroZ( this Vector3 vector3 ) =>
        new( vector3.x, vector3.y, 0 );

    public static Vector3 WithZeroYZ( this Vector3 vector3 ) =>
        new( vector3.x, 0, 0 );

    public static Vector3 WithZeroXZ( this Vector3 vector3 ) =>
        new( 0, vector3.y, 0 );

    public static Vector3 WithZeroXY( this Vector3 vector3 ) =>
        new( 0, 0, vector3.z );

    public static Vector3 WithZeroX( this Vector3 vector3 ) =>
        new( 0, vector3.y, vector3.z );

    public static Vector2 ToVector2WithZY( this Vector3 vector3 ) =>
        new( vector3.z, vector3.y );

    public static Vector3 HadaProduct( this Vector3 a, Vector3 b ) =>
        new( a.x * b.x, a.y * b.y, a.z * b.z );

    public static Vector3 Abs( this Vector3 v ) =>
       new( Mathf.Abs( v.x ), Mathf.Abs( v.y ), Mathf.Abs( v.z ) );
  }
}
