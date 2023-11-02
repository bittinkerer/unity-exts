
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;

namespace Packages.Estenis.UnityExts_
{
    public enum ListDifferenceOptions
    {
        ANY,
        IGNORE_ORDER
    }

    public static class IListExtensions
    {
        //
        ///

        public static List<ListDifference<T>> Differences<T>(
            this IList<T> original,
            IList<T> updated,
            ListDifferenceOptions listDifferenceOptions = ListDifferenceOptions.IGNORE_ORDER)
            where T : IEquatable<T> =>
            listDifferenceOptions switch
            {
                ListDifferenceOptions.IGNORE_ORDER => original.DifferencesIgnoreOrder<T>(updated),
                _ => original.DifferencesAny<T>(updated),
            };

        private static List<ListDifference<T>> DifferencesAny<T>(
            this IList<T> original, IList<T> updated)
        {
            throw new NotImplementedException();
        }

        private static List<ListDifference<T>> DifferencesIgnoreOrder<T>(
            this IList<T> original, IList<T> updated)
            where T : IEquatable<T>
        {
            var originalFiltered = original.Where(c => c != null);
            var updatedFiltered = updated.Where(c => c != null);
            if(!originalFiltered.Any() && !updatedFiltered.Any())
            {
                return new List<ListDifference<T>>();
            }

            HashSet<T> originalSet = new(originalFiltered ?? Enumerable.Empty<T>());
            HashSet<T> updatedSet = new(updatedFiltered ?? Enumerable.Empty<T>());
            var removed = originalSet.Except(updatedSet);
            var added = updatedSet.Except(originalSet);

            return 
                added.Select(a =>
                    new ListDifference<T>
                    {
                        ListChangeType = ListChangeType.ADDED,
                        Item = a
                    })
                .Concat(
                    removed.Select(
                        r => new ListDifference<T> 
                        { 
                            ListChangeType = ListChangeType.REMOVED,
                            Item = r
                        }))
                .ToList();
        }
        
    }
}