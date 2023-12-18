using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engines
{
    public class LimitedLinkedList<T> : IEnumerable<T>
    {
        public LinkedListItem<T> Head { get; set; }
        public LinkedListItem<T> Tail { get; set; }
        public int Count { get; set; }
        public LimitedLinkedList() { }
        public LimitedLinkedList(T data)
        {
            var item = new LinkedListItem<T>(data);
            Head = item;
            Tail = item;
            Count = 1;
        }
        public void AddLast(T data)
        {
            var item = new LinkedListItem<T>(data);
            if (Count == 0)
            {
                Head = item;
                Tail = item;
                Count = 1;
                return;
            }
            Tail.Next = item;
            item.Previous = Tail;
            Tail = item;
            Count++;
        }
        public void Remove(T data)
        {
            var current = Head;;
            while (current.Next != null)
            {
                if(current.Next.Data.Equals(data) && data.Equals(current.Next.Data) && current.Next.Next is null)
                {
                    current.Next = null;
                    Count--;
                    return;
                }
                if (current.Data.Equals(data) && data.Equals(Head.Data))
                {
                    Head = Head.Next;
                    Count--;
                    return;
                }
                if (current.Data.Equals(data))
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                    Count--;
                }
                current = current.Next;
            }
        }
        public IEnumerator GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>)GetEnumerator();
        public void Clear()
        {
            while (Head != null)
            {
                var temp = Head;
                Head = Head.Next;
                temp = null;
            }
            Head = null;
            Tail = null;
            Count = 0;
        }
    }
}
