using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engines
{
    public class HashTableNode<T>
    {
        public T Key { get; private set; }
        public T Value { get; private set; }

        public HashTableNode(T key = default, T value = default)
        {
            Key = key;
            Value = value;
        }
    }
}
