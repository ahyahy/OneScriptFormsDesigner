using System;
using System.Collections.Generic;
using System.Collections;

namespace osfDesigner
{
    public class MyList : IList, IList<ImageEntry>
    {
        List<ImageEntry> _list = new List<ImageEntry>();

        public ImageEntry this[int index]
        {
            get { return ((IList<ImageEntry>)_list)[index]; }
            set { ((IList<ImageEntry>)_list)[index] = value; }
        }

        object IList.this[int index]
        {
            get { return ((IList)_list)[index]; }
            set { ((IList)_list)[index] = value; }
        }

        public int Count
        {
            get { return ((IList<ImageEntry>)_list).Count; }
        }

        public bool IsFixedSize
        {
            get { return ((IList)_list).IsFixedSize; }
        }

        public bool IsReadOnly
        {
            get { return ((IList<ImageEntry>)_list).IsReadOnly; }
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

        public void Add(ImageEntry item)
        {
            ((IList<ImageEntry>)_list).Add(item);
        }

        public void Clear()
        {
            ((IList<ImageEntry>)_list).Clear();
        }

        public bool Contains(object value)
        {
            return ((IList)_list).Contains(value);
        }

        public bool Contains(ImageEntry item)
        {
            return ((IList<ImageEntry>)_list).Contains(item);
        }

        public void CopyTo(Array array, int index)
        {
            ((IList)_list).CopyTo(array, index);
        }

        public void CopyTo(ImageEntry[] array, int arrayIndex)
        {
            ((IList<ImageEntry>)_list).CopyTo(array, arrayIndex);
        }

        public IEnumerator<ImageEntry> GetEnumerator()
        {
            return ((IList<ImageEntry>)_list).GetEnumerator();
        }

        public int IndexOf(object value)
        {
            return ((IList)_list).IndexOf(value);
        }

        public int IndexOf(ImageEntry item)
        {
            return ((IList<ImageEntry>)_list).IndexOf(item);
        }

        public void Insert(int index, object value)
        {
            ((IList)_list).Insert(index, value);
        }

        public void Insert(int index, ImageEntry item)
        {
            ((IList<ImageEntry>)_list).Insert(index, item);
        }

        public void Remove(object value)
        {
            ((IList)_list).Remove(value);
        }

        public bool Remove(ImageEntry item)
        {
            return ((IList<ImageEntry>)_list).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<ImageEntry>)_list).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList)_list).GetEnumerator();
        }
    }
}
