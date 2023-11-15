using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public static class ComponentExtensions
    {
        public static void UnhideInInspector(this Component component)
        {
            if(component == null)
            {
                return;
            }
            component.hideFlags = HideFlags.None;
        }

        public static void HideInInspector(this Component component)
        {
            if(component == null)
            {
                return;
            }
            component.hideFlags |= HideFlags.HideInInspector;
        }
            
    }
}