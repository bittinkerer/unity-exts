using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public static class LayerMaskExtensions
    {
        public static bool ContainsLayer(this int layerMask, int layer) =>
            ((1 << layer) & layerMask) != 0;
    }
}
