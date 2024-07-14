using System.Linq;
using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public static class GameObjectExtensions
    {
        public static GameObject GetRoot(this GameObject go)
        {
            if (go.TryGetComponent<RootGameObject>(out var result))
                return result.gameObject;

            if(go.transform.parent == null)
            {
                return go;
            }

            return GetRoot(go.transform.parent.gameObject);
        }

        public static Component[] GetComponentsRecursive<T>(this GameObject go) where T : Component =>
            go.GetComponents<T>().Concat(go.GetComponentsInChildren<T>()).ToArray();

        public static GameObject[] GetSelfAndAllChildren(this GameObject go)
        {
            var result = new GameObject[] { go };
            if (go.transform.childCount == 0)
            {
                return result;
            }

            foreach (Transform transform in go.transform)
            {
                var childrenGOs = GetSelfAndAllChildren(transform.gameObject);
                result.Add(childrenGOs);
            }
            return result;
        }
    }
}
