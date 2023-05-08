using System.Collections;
using Database.Models;

namespace D9MXN2.Models;

public class PersonContainer<T> : ICollection<T> where T : Person
{
    List<T> _values = new();

    public int Count => _values.Count;

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        _values.Add(item);
    }

    public void Clear()
    {
        _values.Clear();
    }


    public int BinarySearch(T item) {
        this._values.Sort();

        int begin = 0;
        int center = 0;
        int end = this._values.Count() - 1;

        while(begin <= end) {
            center = (begin + end) / 2; // hmm
            
            if(this._values[center] < item) {
                begin = center + 1;
            } else if(this._values[center] > item) {
                end = center - 1;
            } else {
                return center; // FIXME: this will give false positives
            }
        }

        return -1;
    }

    public bool Contains(T item)
    {
        return BinarySearch(item) >= 0;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        this._values.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach(var elem in this._values) {
            yield return elem;
        }
    }

    public bool Remove(T item)
    {
        return this._values.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
