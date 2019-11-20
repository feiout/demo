using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace New.Common
{
    public class DictionaryWrapper<TKey, TValue> : IDictionary<TKey, TValue>, IWrappedDictionary
    {
        private readonly IDictionary _dictionary;
        private readonly IDictionary<TKey, TValue> _genericDictionary;
        private object _syncRoot;

        public ICollection<TKey> Keys
        {
            get
            {
                return _dictionary != null ? Enumerable.ToList(Enumerable.Cast<TKey>(_dictionary.Keys)) : _genericDictionary.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return _dictionary != null ? Enumerable.ToList(Enumerable.Cast<TValue>(_dictionary.Values)) : _genericDictionary.Values;
            }
        }

        public TValue this[TKey key]
        {
            get { return _dictionary != null ? (TValue)_dictionary[key] : _genericDictionary[key]; }
            set
            {
                if (_dictionary != null)
                    _dictionary[key] = value;
                else
                    _genericDictionary[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return _dictionary != null ? _dictionary.Count : _genericDictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _dictionary != null ? _dictionary.IsReadOnly : _genericDictionary.IsReadOnly;
            }
        }

        bool IDictionary.IsFixedSize
        {
            get
            {
                return _genericDictionary == null && _dictionary.IsFixedSize;
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return _genericDictionary != null ? Enumerable.ToList(_genericDictionary.Keys) : _dictionary.Keys;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return _genericDictionary != null ? Enumerable.ToList(_genericDictionary.Values) : _dictionary.Values;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return _dictionary != null && _dictionary.IsSynchronized;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                    Interlocked.CompareExchange(ref _syncRoot, new object(), null);
                return _syncRoot;
            }
        }

        public object UnderlyingDictionary
        {
            get { return _dictionary != null ? (object)_dictionary : _genericDictionary; }
        }

        public DictionaryWrapper(IDictionary dictionary)
        {
            ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
            _dictionary = dictionary;
        }

        public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
        {
            ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
            _genericDictionary = dictionary;
        }

        public void Add(TKey key, TValue value)
        {
            if (_dictionary != null)
                _dictionary.Add(key, value);
            else
                _genericDictionary.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            if (_dictionary != null)
                return _dictionary.Contains(key);
            return _genericDictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            if (_dictionary == null)
                return _genericDictionary.Remove(key);
            if (!_dictionary.Contains(key))
                return false;
            _dictionary.Remove(key);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_dictionary == null)
                return _genericDictionary.TryGetValue(key, out value);
            if (!_dictionary.Contains(key))
            {
                value = default(TValue);
                return false;
            }
            value = (TValue)_dictionary[key];
            return true;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (_dictionary != null)
                ((IList)_dictionary).Add(item);
            else
                _genericDictionary.Add(item);
        }

        public void Clear()
        {
            if (_dictionary != null)
                _dictionary.Clear();
            else
                _genericDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (_dictionary != null)
                return ((IList)_dictionary).Contains(item);
            return _genericDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (_dictionary != null)
            {
                foreach (DictionaryEntry dictionaryEntry in _dictionary)
                    array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)dictionaryEntry.Key, (TValue)dictionaryEntry.Value);
            }
            else
                _genericDictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (_dictionary == null)
                return _genericDictionary.Remove(item);
            if (!_dictionary.Contains(item.Key))
                return true;
            if (!Equals(_dictionary[item.Key], item.Value))
                return false;
            _dictionary.Remove(item.Key);
            return true;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (_dictionary != null)
                return Enumerable.Select(Enumerable.Cast<DictionaryEntry>(_dictionary), de => new KeyValuePair<TKey, TValue>((TKey)de.Key, (TValue)de.Value)).GetEnumerator();
            return _genericDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void IDictionary.Add(object key, object value)
        {
            if (_dictionary != null)
                _dictionary.Add(key, value);
            else
                _genericDictionary.Add((TKey)key, (TValue)value);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            if (_dictionary != null)
                return _dictionary.GetEnumerator();
            return new DictionaryEnumerator<TKey, TValue>(_genericDictionary.GetEnumerator());
        }

        bool IDictionary.Contains(object key)
        {
            if (_genericDictionary != null)
                return _genericDictionary.ContainsKey((TKey)key);
            return _dictionary.Contains(key);
        }

        public void Remove(object key)
        {
            if (_dictionary != null)
                _dictionary.Remove(key);
            else
                _genericDictionary.Remove((TKey)key);
        }

        object IDictionary.this[object key]
        {
            get
            {
                if (_dictionary != null)
                    return _dictionary[key];
                return _genericDictionary[(TKey)key];
            }
            set
            {
                if (_dictionary != null)
                    _dictionary[key] = value;
                else
                    _genericDictionary[(TKey)key] = (TValue)value;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (_dictionary != null)
                _dictionary.CopyTo(array, index);
            else
                _genericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
        }

        private struct DictionaryEnumerator<TEnumeratorKey, TEnumeratorValue> : IDictionaryEnumerator
        {
            private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;

            public DictionaryEntry Entry
            {
                get
                {
                    return (DictionaryEntry)Current;
                }
            }

            public object Key
            {
                get
                {
                    return Entry.Key;
                }
            }

            public object Value
            {
                get
                {
                    return Entry.Value;
                }
            }

            public object Current
            {
                get
                {
                    return new DictionaryEntry(_e.Current.Key, _e.Current.Value);
                }
            }

            public DictionaryEnumerator(IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
            {
                ValidationUtils.ArgumentNotNull(e, "e");
                _e = e;
            }

            public bool MoveNext()
            {
                return _e.MoveNext();
            }

            public void Reset()
            {
                _e.Reset();
            }
        }
    }
}
