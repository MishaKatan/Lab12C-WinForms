using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engines
{
    public class LinkedListItem <T>
    {
        public T Data { get; set; }
        public LinkedListItem<T> Next { get; set; }
        public LinkedListItem<T> Previous { get; set; }
        public LinkedListItem(T data)
        {
            Data = data;
        }
        public override string ToString() => Data + " ";
    }
}
