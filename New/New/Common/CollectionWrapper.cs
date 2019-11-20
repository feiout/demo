using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace New.Common
{
    public class CollectionWrapper<T> : ICollection<T>, IWrappedCollection
    {
        private readonly IList _list;
        private readonly ICollection<T> _genericCollection;
        private object _syncRoot;

        public virtual int Count
        {
            get
            {
                return _genericCollection != null ? _genericCollection.Count : _list.Count;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return _genericCollection != null ? _genericCollection.IsReadOnly : _list.IsReadOnly;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return _genericCollection != null ? _genericCollection.IsReadOnly : _list.IsFixedSize;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
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

        public object UnderlyingCollection
        {
            get { return _genericCollection != null ? (object)_genericCollection : _list; }
        }

        public CollectionWrapper(IList list)
        {
            ValidationUtils.ArgumentNotNull(list, "list");
            if (list is ICollection<T>)
                _genericCollection = (ICollection<T>)list;
            else
                _list = list;
        }

        public CollectionWrapper(ICollection<T> list)
        {
            ValidationUtils.ArgumentNotNull(list, "list");
            _genericCollection = list;
        }

        public virtual void Add(T item)
        {
            if (_genericCollection != null)
                _genericCollection.Add(item);
            else
                _list.Add(item);
        }

        public virtual void Clear()
        {
            if (_genericCollection != null)
                _genericCollection.Clear();
            else
                _list.Clear();
        }

        public virtual bool Contains(T item)
        {
            return _genericCollection != null ? _genericCollection.Contains(item) : _list.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            if (_genericCollection != null)
                _genericCollection.CopyTo(array, arrayIndex);
            else
                _list.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(T item)
        {
            if (_genericCollection != null)
                return _genericCollection.Remove(item);
            bool flag = _list.Contains(item);
            if (flag)
                _list.Remove(item);
            return flag;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return _genericCollection != null ? _genericCollection.GetEnumerator() : Enumerable.Cast<T>(_list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _genericCollection != null ? _genericCollection.GetEnumerator() : _list.GetEnumerator();
        }

        int IList.Add(object value)
        {
            VerifyValueType(value);
            Add((T)value);
            return Count - 1;
        }

        bool IList.Contains(object value)
        {
            return IsCompatibleObject(value) && Contains((T)value);
        }

        int IList.IndexOf(object value)
        {
            if (_genericCollection != null)
                throw new InvalidOperationException("Wrapped ICollection<T> does not support IndexOf.");
            return IsCompatibleObject(value) ? _list.IndexOf((T)value) : -1;
        }

        void IList.RemoveAt(int index)
        {
            if (_genericCollection != null)
                throw new InvalidOperationException("Wrapped ICollection<T> does not support RemoveAt.");
            _list.RemoveAt(index);
        }

        public object this[int index]
        {
            get
            {
                if (_genericCollection != null)
                    throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
                return _list[index];
            }
            set
            {
                if (_genericCollection != null)
                    throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
                VerifyValueType(value);
                _list[index] = (T)value;
            }
        }

        void IList.Insert(int index, object value)
        {
            if (_genericCollection != null)
                throw new InvalidOperationException("Wrapped ICollection<T> does not support Insert.");
            VerifyValueType(value);
            _list.Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            if (!IsCompatibleObject(value))
                return;
            Remove((T)value);
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            CopyTo((T[])array, arrayIndex);
        }

        private static void VerifyValueType(object value)
        {
            if (!IsCompatibleObject(value))
                throw new ArgumentException(StringUtils.FormatWith("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.", CultureInfo.InvariantCulture, value, typeof(T)), "value");
        }

        private static bool IsCompatibleObject(object value)
        {
            return value is T || value == null && (!TypeExtensions.IsValueType(typeof(T)) || ReflectionUtils.IsNullableType(typeof(T)));
        }
    }
}
