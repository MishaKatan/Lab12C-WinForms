using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Engines
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
    public class CollectionHandlerEventArgs
    {
        public string? CollectionChange { get; set; } // свойство с информацией об изменениях в коллекции
        public string? ObjectReference { get; set; } // свойство со ссылкой на объект, с которым связаны изменения

        public CollectionHandlerEventArgs(string? collectionChange, object? objectReference)
        {
            CollectionChange = collectionChange;
            ObjectReference = objectReference.ToString();
        }
    }
    public class SuperSearchTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        public event CollectionHandler CollectionCountChanged; // событие добавления / удаления элемента
        public event CollectionHandler CollectionReferenceChanged; // событие изменения ссылки элемента коллекции
        public string Name { get; set; }

        public SuperSearchTree(string name) : base() => Name = name;

        public void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args) => CollectionCountChanged?.Invoke(source, args);// обработчик событий

        public void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args) => CollectionReferenceChanged?.Invoke(source, args);

        public override void Add(T item)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs("В дерево поиска добавлен новый элемент", item));
            base.Add(item);
        }
        public override bool Remove(T item)
        {
            if (Contains(item) < 0)
            {
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs("Неудачная попытка удаления элемента", item));
                return false;
            }

            OnCollectionCountChanged(this, new CollectionHandlerEventArgs("Из дерева удален элемент", item));
            return base.Remove(item);
        }
        public T this[int index]
        {
            get => base[index];

            set
            {
                base.Remove(this[index]);
                base.Add(value);
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs("В коллекции был изменен элемент", value));
            }
        }        
    }
    public class JournalEntry
    {
        public string CollectionName { get; set; } // название коллекции, в которой произошло событие
        public string CollectionChange { get; set; } // тип изменения в коллекции
        public string ObjectData { get; set; } // данные объекта

        public JournalEntry(string collectionName, string collectionChange, string objectData)
        {
            CollectionName = collectionName;
            CollectionChange = collectionChange;
            ObjectData = objectData;
        }

        public override string ToString()
        {
            DateTime now = DateTime.Now;
            string data = now.ToString("dd/MM/yyyy HH:mm:ss");
            return $"[{data}] В дереве '{CollectionName}' произошло событие: '{CollectionChange}'.\nДанные объекта:\n{ObjectData}";
        }
    }
    public class Journal
    {
        public List<JournalEntry> list = null;

        public Journal() => list = new List<JournalEntry>();

        public void Writer(object source, CollectionHandlerEventArgs args)
        {
            JournalEntry elem = new JournalEntry(((SuperSearchTree<Engine>)source).Name, args.CollectionChange, args.ObjectReference);
            list.Add(elem);
        }
    }
}
