using System.Linq;
using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public static class GameObjectExtensions
    {
        public static GameObject GetRoot(this GameObject go)
        {
            var result = go.GetComponent<RootGameObject>();
            if (result != null)
                return result.gameObject;

            if(go.transform.parent == null)
            {
                return go;
            }

            return GetRoot(go.transform.parent.gameObject);
        }

        public static Component[] GetComponentsRecursive<T>(this GameObject go) where T : Component =>
            go.GetComponents<T>().Concat(go.GetComponentsInChildren<T>()).ToArray();
    }
}
