using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Collections
{
    public class MyList<T> : IList<T>
    {
        private const int InitialCapacity = 16;
        private const int CapacityGrowthFactor = 2;

        private T[] _core;
        private int _count = 0;
        private int _version = int.MinValue;
        private Enumerator _enumerator;

        public int Capacity => _core.Length;
        public int Count => _count;
        public bool IsReadOnly => false;

        public MyList() : this(InitialCapacity) { }

        public MyList(int capacity)
        {
            _core = new T[InitialCapacity];
        }



        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return _core[index];
            }

            set
            {
                CheckIndex(index);
                _core[index] = value;
                _version++;
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (Equals(item, _core[i])) return i;
            }
            return -1;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void Add(T item)
        {
            IncreaseCount();
            _core[_count - 1] = item;
            _version++;
        }

        public void Insert(int index, T item)
        {
            CheckIndex(index);
            IncreaseCount();
            Array.Copy(_core, index, _core, index + 1, _count - index - 1);
            _core[index] = item;
            _version++;
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (Equals(item, _core[i]))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);
            if (index < _count - 1)
                Array.Copy(_core, index + 1, _core, index, _count - index - 1);
            _core[_count - 1] = default(T);
            _count--;
            _version++;
        }

        public void Clear()
        {
            Array.Clear(_core, 0, _count);
            _count = 0;
            _version++;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_core, 0, array, arrayIndex, _count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            if (_count == 0) return "Empty list";

            var builder = new StringBuilder();
            for (int i = 0; i < _count; i++)
            {
                builder.Append(_core[i]);
                builder.Append(", ");
            }
            return builder.ToString(0, builder.Length - 2);
        }



        private void CheckIndex(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException();
        }

        private void IncreaseCount()
        {
            if (_core.Length <= _count)
            {
                T[] @new;
                if (_core.Length > 0)
                {
                    @new = new T[_core.Length * CapacityGrowthFactor];
                    Array.Copy(_core, @new, _core.Length);
                }
                else
                {
                    @new = new T[1];
                }
                _core = @new;
            }
            _count++;
        }



        private struct Enumerator : IEnumerator<T>
        {
            private readonly MyList<T> _source;

            private int _index;
            private readonly int _version;

            public T Current
            {
                get
                {
                    Validate();
                    if (_index >= _source.Count)
                        throw new InvalidOperationException("No more elements in the collection.");
                    if (_index == -1)
                        throw new InvalidOperationException("MoveNext method should be called before accessing Current property.");
                    return _source[_index];
                }
            }
            object IEnumerator.Current => Current;



            public Enumerator(MyList<T> source)
            {
                _source = source;
                _index = -1;
                _version = source._version;
            }



            public void Dispose() { }

            public bool MoveNext()
            {
                Validate();
                return ++_index < _source._count;
            }

            public void Reset()
            {
                Validate();
                _index = -1;
            }

            public override string ToString()
            {
                return _index < 0 ? "Not initialized" : 
                    _index < _source.Count ? Current.ToString() : "Finished enumerating";
            }



            private void Validate()
            {
                if (this._version != _source._version)
                    throw new InvalidOperationException("Collection was modified.");
            }
        }
    }
}
