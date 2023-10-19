using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public static class ComponentExtensions
    {
        public static void HideInInspector(this Component component) =>
            component.hideFlags &= ~HideFlags.HideInInspector;

        public static void UnhideInInspector(this Component component) =>
            component.hideFlags |= HideFlags.HideInInspector;
    }
}