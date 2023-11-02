
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            where T : IEquatable<T>, INullable =>
            listDifferenceOptions switch
            {
                ListDifferenceOptions.IGNORE_ORDER => original.DifferencesIgnoreOrder<T>(updated),
                _ => original.DifferencesAny<T>(updated),
            };

        private static List<ListDifference<T>> DifferencesAny<T>(
            this IList<T> original, IList<T> updated)
            where T : IEquatable<T>, INullable
        {
            throw new NotImplementedException();
        }

        private static List<ListDifference<T>> DifferencesIgnoreOrder<T>(
            this IList<T> original, IList<T> updated)
            where T : IEquatable<T>, INullable
        {
            var originalFiltered = original.Where(c => c != null && !c.IsNull);
            var updatedFiltered = updated.Where(c => c != null && !c.IsNull);
            if(!originalFiltered.Any() && !updatedFiltered.Any())
            {
                return new List<ListDifference<T>>();
            }

            HashSet<T> originalSet = new(originalFiltered ?? Enumerable.Empty<T>());
            HashSet<T> updatedSet = new(updatedFiltered ?? Enumerable.Empty<T>());
            var removed = originalSet.Except(updatedSet).ToList();
            var added = updatedSet.Except(originalSet).ToList();

            var result = 
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
            return result;
        }
        
    }
}