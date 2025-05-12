using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
  public static class BoxColliderExtensions
  {
    public static Vector3 WorldCenterPosition(this BoxCollider boxCollider) => boxCollider.bounds.center;

    public static Vector3 WorldLeftPosition(this BoxCollider boxCollider) => 
      WorldCenterPosition(boxCollider) - new Vector3(0, 0, BoxSize(boxCollider).z / 2);

    public static Vector3 WorldRightPosition(this BoxCollider boxCollider) => 
      WorldCenterPosition(boxCollider) + new Vector3(0, 0, BoxSize(boxCollider).z / 2);

    public static Vector3 WorldBottomPosition(this BoxCollider boxCollider) => 
      WorldCenterPosition(boxCollider) - new Vector3(0, BoxSize(boxCollider).y / 2, 0);

    public static Vector3 WorldTopPosition(this BoxCollider boxCollider) => 
      WorldCenterPosition(boxCollider) + new Vector3(0, BoxSize(boxCollider).y / 2, 0);

    public static Vector3 BoxSize(this BoxCollider boxCollider) => 
      boxCollider.size.HadaProduct( boxCollider.gameObject.transform.localScale ); // account for gameobject scale
  }
}
