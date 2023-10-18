///
/// https://stackoverflow.com/questions/67578533/serialize-observablecollection-like-class-observablelist-in-unity-c-sharp
///
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public abstract class ObservableList
    {
    }

    [Serializable]
    public class ObservableList<T> : ObservableList, IList<T>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        [SerializeField] private List<T> _list = new();

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _list.Add(item);

            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, _list.Count - 1);
        }

        public void Clear()
        {
            _list.Clear();

            OnCollectionReset();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            var itemIndex = _list.IndexOf(item);
            var doExist = itemIndex != -1;
            var didRemove = _list.Remove(item);
            if (doExist && didRemove)
            {
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, itemIndex);
            }

            return didRemove;
        }

        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);

            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);

            var item = _list[index];

            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            // _list.AddRange(collection);
            InsertRange(_list.Count, collection);
        }

        public void RemoveAll(Predicate<T> predicate)
        {
            // _list.RemoveAll(predicate);
            int index = 0;
            while (index < _list.Count)
            {
                if (predicate(_list[index]))
                {
                    RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            // _list.InsertRange(index, collection);
            foreach (var item in collection)
            {
                Insert(index, item);
                index++;
            }
        }

        public void RemoveRange(int index, int count)
        {
            // _list.RemoveRange(index, count);
            if (index < 0 || _list.Count < index + count - 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < count; i++)
            {
                RemoveAt(index);
            }
        }

        public T this[int index]
        {
            get => _list[index];
            set
            {
                var oldValue = _list[index];
                _list[index] = value;

                OnCollectionChanged(NotifyCollectionChangedAction.Replace, oldValue, value, index);
            }
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index) =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        private void OnCollectionReset() =>
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        /// <summary>
        /// Not-LINQ lambda foreach
        /// </summary>
        public void ForEach(Action<T> action)
        {
            foreach (var cur in _list)
            {
                action(cur);
            }
        }
    }
}