using System.Collections;

namespace HetedikOra.Enumerable.UsingYield
{
    public class IntegerCollection : IEnumerable<int>
    {
        private int[] _items;
        private int _count;

        public IntegerCollection()
        {
            _items = new int[4];
            _count = 0;
        }

        public void Add(int item)
        {
            if (_count == _items.Length)
            {
                EnsureCapacity();
            }

            _items[_count++] = item;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void EnsureCapacity()
        {
            var newItems = new int[_items.Length * 2];

            Array.Copy(_items, newItems, _count);

            _items = newItems;
        }
    }
}
