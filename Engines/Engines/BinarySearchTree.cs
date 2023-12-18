using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Engines
{
    public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private TreeNode<T>? root; // Root of BinarySearchTree
        public TreeNode<T>? Root
        {
            get { return root; }
            set { root = value; }
        }
        public int Count { get; private set; }
        public BinarySearchTree() // пустая коллекция 
        {
            root = null;
        }
        public BinarySearchTree(int count) // создание с заданной емкостью 
        {
            Count = count;
            root = Gen(count, root);
        }
        public BinarySearchTree(BinarySearchTree<T> tree) 
        {
            root = tree.root.Clone();
        }
        public virtual T this[int i]
        {
            get
            {
                int currentIndex = 0;
                foreach (T value in this)
                {
                    if (currentIndex == i)
                        return value;
                    currentIndex++;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public virtual TreeNode<T> Gen(int count, TreeNode<T>? root) // генерация дерева
        {
            if (count == 0) // условие выхода из рекурсии
                return root;

            int left = count / 2; // производим балансировку дерева, расчитываем количество левых узлов (нод)
            int right = count - left - 1; // не учитываем корень дерева (рут) - вычитаем единицу

            Engine motor1 = new Engine();
            motor1.RandomInit(); // генерируем рандомный элемент
            root = new TreeNode<T>((T)(object)motor1); // корень дерева

            root.Left = Gen(left, root.Left); // рекурсивно вызываем метод для генерации левой ветки
            root.Right = Gen(right, root.Right); // генерируем правую ветку
            return root;
        }
        public virtual void Add(T value)
        {
            // If the tree is empty, create a new node and make it the root
            if (Root is null)
            {
                root = new TreeNode<T>(value);
                Count++;
                return;
            }
            // Find the appropriate position to insert the new node
            TreeNode<T> current = Root;
            TreeNode<T> parent = current;

            bool isLeft = false;
            while (current is not null)
            {
                parent = current;
                if (value.CompareTo(current.Data) > 0)
                {
                    // Go left
                    current = current.Left;
                    isLeft = true;
                }
                else
                {
                    // Go right
                    current = current.Right;
                    isLeft = false;
                }
            }
            TreeNode<T> newNode = new(value);
            if (isLeft)
                parent.Left = newNode;
            else
                parent.Right = newNode;
            Count++;
        }
        public virtual void ShowByLevels() // вывод дерева по уровням
        {
            if (root != null)
                ShowByLevelsHelper(root, 3);
            else
                Console.WriteLine("Дерево не содержит элементов");
        }
        private void ShowByLevelsHelper(TreeNode<T> node, int l) // вспомогательный рекурсивный метод для отображения дерева по уровням
        {
            if (node != null)
            {
                ShowByLevelsHelper(node.Left, l + 3);
                for (int i = 0; i < l; i++)
                    Console.Write(" ");
                
                Engine motor = (Engine)(object)node.Data;
                if (motor is not null)
                    Console.WriteLine(motor.Power);
                ShowByLevelsHelper(node.Right, l + 3);
            }
        }
        public virtual bool Remove(T value) => Remove(root, value);
        private bool Remove(TreeNode<T> node, T value)
        {
            int compare = value.CompareTo(node.Data);
            if (compare == 0)
            {
                List<T> list = new List<T>();
                foreach(var item in this)
                {
                    if(!item.Equals(node.Data))
                        list.Add(item);
                    list.Sort();
                }
                Count--;
                root = Rebalance(list, 0, list.Count - 1);
                return true;
            }
            if (compare > 0)
                return Remove(node.Left,value);
            return Remove(node.Right, value);
        }
        private static TreeNode<T> Rebalance(List<T> values, int start, int end)
        {
            if (start > end)
                return null;

            int mid = (start + end) / 2;
            TreeNode<T> node = new TreeNode<T>(values[mid]);
            node.Right = Rebalance(values, start, mid - 1);
            node.Left = Rebalance(values, mid + 1, end);

            return node;
        }
        public virtual int Contains(T value) => Contains(root, value);
        private int Contains(TreeNode<T> node, T value)
        {
            if (node == null)
                return -1;
            int compare = value.CompareTo(node.Data);
            if (compare == 0)
            {
                value = node.Data;
                int currentIndex = 0;
                foreach (var item in this)
                {
                    if (item.Equals(value))
                        return currentIndex;
                    currentIndex++;
                }
            }
            if (compare > 0)
                return Contains(node.Left, value);
            return Contains(node.Right, value);
        }
        public T ShallowCopy() => (T)this.MemberwiseClone();
        public BinarySearchTree<T> Clone()
        {
            BinarySearchTree<T> tree = new BinarySearchTree<T>();

            tree.root = Root.Clone();

            return tree;
        }
        public void Clear() => root = null;
        public IEnumerator<T> GetEnumerator()
        {
            if (root != null)
            {
                Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
                TreeNode<T> current = root;
                bool goLeftNext = true;
                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Data;

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
