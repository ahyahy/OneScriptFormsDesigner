using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System;

namespace osfDesigner
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MyAnnuallyBoldedDatesList : IList, IList<DateEntry>
    {
        List<DateEntry> _list = new List<DateEntry>();

        public DateEntry this[int index]
        {
            get { return ((IList<DateEntry>)_list)[index]; }
            set { ((IList<DateEntry>)_list)[index] = value; }
        }

        object IList.this[int index]
        {
            get { return ((IList)_list)[index]; }
            set { ((IList)_list)[index] = value; }
        }

        public int Count
        {
            get { return ((IList<DateEntry>)_list).Count; }
        }

        public bool IsFixedSize
        {
            get { return ((IList)_list).IsFixedSize; }
        }

        public bool IsReadOnly
        {
            get { return ((IList<DateEntry>)_list).IsReadOnly; }
        }

        public bool IsSynchronized
        {
            get { return ((IList)_list).IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return ((IList)_list).SyncRoot; }
        }

        public int Add(object value)
        {
            return ((IList)_list).Add(value);
        }

        public void Add(DateEntry item)
        {
            ((IList<DateEntry>)_list).Add(item);
        }

        public void Clear()
        {
            ((IList<DateEntry>)_list).Clear();
        }

        public bool Contains(object value)
        {
            return ((IList)_list).Contains(value);
        }

        public bool Contains(DateEntry item)
        {
            return ((IList<DateEntry>)_list).Contains(item);
        }

        public void CopyTo(Array array, int index)
        {
            ((IList)_list).CopyTo(array, index);
        }

        public void CopyTo(DateEntry[] array, int arrayIndex)
        {
            ((IList<DateEntry>)_list).CopyTo(array, arrayIndex);
        }

        public IEnumerator<DateEntry> GetEnumerator()
        {
            return ((IList<DateEntry>)_list).GetEnumerator();
        }

        public int IndexOf(object value)
        {
            return ((IList)_list).IndexOf(value);
        }

        public int IndexOf(DateEntry item)
        {
            return ((IList<DateEntry>)_list).IndexOf(item);
        }

        public void Insert(int index, object value)
        {
            ((IList)_list).Insert(index, value);
        }

        public void Insert(int index, DateEntry item)
        {
            ((IList<DateEntry>)_list).Insert(index, item);
        }

        public void Remove(object value)
        {
            ((IList)_list).Remove(value);
        }

        public bool Remove(DateEntry item)
        {
            return ((IList<DateEntry>)_list).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<DateEntry>)_list).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList)_list).GetEnumerator();
        }
    }
}
