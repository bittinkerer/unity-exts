namespace Packages.Estenis.UnityExts_
{
    public static class ArrayExtensions
    {
        public static void Add<T>(this T[] a, T[] b)
        {
            var z = new T[a.Length + b.Length];
            a.CopyTo(z, 0);
            b.CopyTo(z, a.Length);
            a = z;
        }
    }
}