using System.Collections;

namespace HetedikOra.Enumerable.UsingEnumerator
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
            return new Enumerator(this);
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

        private sealed class Enumerator : IEnumerator<int>
        {
            private readonly IntegerCollection _collection;

            private int _index;

            public int Current => _collection._items[_index];

            public Enumerator(IntegerCollection collection)
            {
                _collection = collection;
                _index = -1;
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _collection._count;
            }

            public void Reset()
            {
                _index = -1;
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current => Current;
        }
    }
}
