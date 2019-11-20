using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New.Common
{
    public class ListWrapper<T> : CollectionWrapper<T>, IList<T>, IWrappedList
    {
        private readonly IList<T> _genericList;

        public new T this[int index]
        {
            get { return _genericList != null ? _genericList[index] : (T)base[index]; }
            set
            {
                if (_genericList != null)
                    _genericList[index] = value;
                else
                    base[index] = value;
            }
        }

        public override int Count
        {
            get
            {
                return _genericList != null ? _genericList.Count : base.Count;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return _genericList != null ? _genericList.IsReadOnly : base.IsReadOnly;
            }
        }

        public object UnderlyingList
        {
            get { return _genericList ?? UnderlyingCollection; }
        }

        public ListWrapper(IList list) : base(list)
        {
            ValidationUtils.ArgumentNotNull(list, "list");
            if (!(list is IList<T>))
                return;
            _genericList = (IList<T>)list;
        }

        public ListWrapper(IList<T> list) : base(list)
        {
            ValidationUtils.ArgumentNotNull(list, "list");
            _genericList = list;
        }

        public int IndexOf(T item)
        {
            return _genericList != null ? _genericList.IndexOf(item) : IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (_genericList != null)
                _genericList.Insert(index, item);
            else
                Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            if (_genericList != null)
                _genericList.RemoveAt(index);
            else
                RemoveAt(index);
        }

        public override void Add(T item)
        {
            if (_genericList != null)
                _genericList.Add(item);
            else
                base.Add(item);
        }

        public override void Clear()
        {
            if (_genericList != null)
                _genericList.Clear();
            else
                base.Clear();
        }

        public override bool Contains(T item)
        {
            return _genericList != null ? _genericList.Contains(item) : base.Contains(item);
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            if (_genericList != null)
                _genericList.CopyTo(array, arrayIndex);
            else
                base.CopyTo(array, arrayIndex);
        }

        public override bool Remove(T item)
        {
            if (_genericList != null)
                return _genericList.Remove(item);
            bool flag = base.Contains(item);
            if (flag)
                base.Remove(item);
            return flag;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return _genericList != null ? _genericList.GetEnumerator() : base.GetEnumerator();
        }
    }
}
