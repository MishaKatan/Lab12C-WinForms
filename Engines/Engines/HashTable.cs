using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engines
{
    public class HashTable<T> : IEnumerable<T>
    {
        public int Size { get; private set; }
        private T[] table;
        private double Load { get; set; }
        public int Count { get; private set; }
        public HashTable(int size)
        {
            Size = size;
            table = new T[size];
            Count = 0;
            Load = 0;
        }
        public HashTable() { }
        public void Add(T value)
        {
            int index = value.GetHashCode() % Size;

            while (table[index] != null)
            {
                index = (index + 1) % Size;
            }
            Count++;
            Load = (double)Count / (double)Size;
            if (Load > 0.75)
                Resize();               
            table[index] = value;
        }
        private void Resize() // перестроение таблицы
        {
            Size *= 2; // увеличние таблицы в 2 раза
            T[] newTable = new T[Size];
            for (int i = 0; i < table.Length; i++) // перебор элементов предыдущей таблицы
            {
                if (table[i] != null)
                {
                    int newIndex = i; 
                    newTable[newIndex] = table[i];
                }
            }
            table = newTable; // присваивание ссылки на новую таблицу
        }
        public int Contains(T item)
        {
            int key = GetHash(item);
            if (table[key] != null)
            {
                for(int i = key; i < Size;i++)
                {
                    if(table[i].Equals(item))
                        return i;
                    if (table[i] == null)
                        return -1;
                }
                return -1;
            }
            else
                return -1;
        }
        public int Contains(double item)
        {
            Engine motor = new Engine();
            motor.Power = item;
                for (int i = 0; i < Size; i++)
                {
                    if(table[i] is Engine engine)
                    if (engine.Power.Equals(motor.Power))
                        return i;
                }
                return -1;
        }
        public void Remove(int index)
        {
            T[] newTable = new T[Size];
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null && i!=index)
                {
                    int newIndex = i;
                    newTable[newIndex] = table[i];
                }
            }
            table = newTable; // присваивание ссылки на новую таблицу
            Count--;
        }
        private int GetHash(T item) => item.GetHashCode() % table.Length;
        //private int GetHash(Engine item) => item.GetHashCode() % table.Length;
        public IEnumerator GetEnumerator()
        {
            foreach (var stuff in table)
                if (stuff != null)
                  yield return stuff;
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>)GetEnumerator();
    }
}
